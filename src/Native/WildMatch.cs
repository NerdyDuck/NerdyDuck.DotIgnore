#region Copyright
/*******************************************************************************
 * NerdyDuck.DotIgnore - A library that implements a file filter using the
 * patterns of .gitignore.
 * 
 * Copyright 2018 Daniel Kopp, dak@nerdyduck.de
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 * Original authors:
 * Written by Rich $alz, mirror!rs, Wed Nov 26 19:03:17 EST 1986.
 * Rich $alz is now <rsalz@bbn.com>.
 * 
 * Modified by Wayne Davison to special-case '/' matching, to make '**'
 * work differently than '*', and to fix the character-class code.
 ******************************************************************************/
#endregion

using System;
using System.Linq;
using System.Globalization;
// Alias to require less changes to dowild() when porting from C.
using uchar = System.Char;

namespace NerdyDuck.DotIgnore.Native
{
	/// <summary>
	/// Provides methods to do shell-style pattern matching.
	/// </summary>
	/// <remarks>This is a port of the original wildmatch.c of git, commit 55d3426 on Jun 24 2017. https://github.com/git/git/blob/55d3426929d4d8c3dec402cabe6fb1bf27d6abad/wildmatch.c</remarks>
	internal static class WildMatch
	{
		#region Constants
		/// <summary>
		/// Flag for <see cref="wildmatch(string, string, int)"/>: Compare text and path names to patterns in a case-insensitive manner.
		/// </summary>
		internal const int WM_CASEFOLD = 1;

		/// <summary>
		/// Flag for <see cref="wildmatch(string, string, int)"/>: Text is a path name, use special ** syntax.
		/// </summary>
		internal const int WM_PATHNAME = 2;

		/// <summary>
		/// Return value for <see cref="wildmatch(string, string, int)"/>: Pattern is malformed.
		/// </summary>
		internal const int WM_ABORT_MALFORMED = 2;

		/// <summary>
		/// Return value for <see cref="wildmatch(string, string, int)"/>: Text does not match to pattern.
		/// </summary>
		internal const int WM_NOMATCH = 1;

		/// <summary>
		/// Return value for <see cref="wildmatch(string, string, int)"/>: Text matches pattern.
		/// </summary>
		internal const int WM_MATCH = 0;

		/// <summary>
		/// Return value for <see cref="wildmatch(string, string, int)"/>: No match. Text was shorter than pattern.
		/// </summary>
		internal const int WM_ABORT_ALL = -1;

		/// <summary>
		/// Return value for <see cref="wildmatch(string, string, int)"/>: No match. Text ran out during ** match
		/// </summary>
		internal const int WM_ABORT_TO_STARSTAR = -2;

		private const char NEGATE_CLASS = '!';
		private const char NEGATE_CLASS2 = '^';

		//private const char GIT_GLOB_SPECIAL = 'G'; // Original is 0x08, as a matching enum with value G; we need only one value, so this will do.

		//private static readonly char[] sane_ctype = new char[256] {
		//	'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'Z', 'Z', 'X', 'X', 'Z', 'X', 'X',		/*   0.. 15 */
		//	'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X',		/*  16.. 31 */
		//	'S', 'P', 'P', 'P', 'R', 'P', 'P', 'P', 'R', 'R', 'G', 'R', 'P', 'P', 'R', 'P',		/*  32.. 47 */
		//	'D', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'D', 'P', 'P', 'P', 'P', 'P', 'G',		/*  48.. 63 */
		//	'P', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A',		/*  64.. 79 */
		//	'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'G', 'G', 'U', 'R', 'P',		/*  80.. 95 */
		//	'P', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A',		/*  96..111 */
		//	'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'R', 'U', 'P', 'X',		/* 112..127 */
		//	'\0' , '\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,
		//	'\0' , '\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,
		//	'\0' , '\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,
		//	'\0' , '\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,
		//	'\0' , '\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,
		//	'\0' , '\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,
		//	'\0' , '\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,
		//	'\0' , '\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0' ,'\0'
		//};

		// Replacement for GIT_GLOB_SPECIAL and sane_ctype, as we only need to check for those 4 characters.
		internal static readonly char[] GIT_GLOB_SPECIAL = new uchar[] { '*', '?', '[', '\\' };
		#endregion

		#region Methods
		/// <summary>
		/// Do shell-style pattern matching for ?, \, [], *, and ** characters.
		/// </summary>
		/// <param name="pattern">The pattern to match to.</param>
		/// <param name="text">The text to match to the pattern.</param>
		/// <param name="flags"></param>
		/// <returns></returns>
		internal static int wildmatch(string pattern, string text, int flags)
		{
			if (pattern == null)
			{
				throw new ArgumentNullException(nameof(pattern));
			}
			if (text == null)
			{
				throw new ArgumentNullException(nameof(text));
			}

			int returnValue = 0;
			pattern = pattern + '\0';
			text = text + '\0';
			unsafe
			{
				fixed (char* ppattern = pattern, ptext = text)
				{
					returnValue = dowild(ppattern, ptext, (uint)flags);
				}
			}

			return returnValue;
		}
		#endregion

		#region Private methods
		#region dowild
		private unsafe static int dowild(char* p, char* text, uint flags)
		{
			uchar p_ch;
			uchar* pattern = p;

			for (; (p_ch = *p) != '\0'; text++, p++)
			{
				int matched, match_slash, negated;
				uchar t_ch, prev_ch;
				if ((t_ch = *text) == '\0' && p_ch != '*')
					return WM_ABORT_ALL;
				if ((flags & WM_CASEFOLD) != 0 && ISUPPER(t_ch))
					t_ch = tolower(t_ch);
				if ((flags & WM_CASEFOLD) != 0 && ISUPPER(p_ch))
					p_ch = tolower(p_ch);
				switch (p_ch)
				{
					case '\\':
						/* Literal match with following character.  Note that the test
						 * in "default" handles the p[1] == '\0' failure case. */
						p_ch = *++p;
						/* NO FALLTHROUGH IN C# - copying code from default */
						if (t_ch != p_ch)
							return WM_NOMATCH;
						continue;
					default:
						if (t_ch != p_ch)
							return WM_NOMATCH;
						continue;
					case '?':
						/* Match anything but '/'. */
						if ((flags & WM_PATHNAME) != 0 && t_ch == '/')
							return WM_NOMATCH;
						continue;
					case '*':
						if (*++p == '*')
						{
							uchar* prev_p = p - 2;
							while (*++p == '*') { }
							if ((flags & WM_PATHNAME) == 0)
								/* without WM_PATHNAME, '*' == '**' */
								match_slash = 1;
							else if ((prev_p < pattern || *prev_p == '/') &&
								(*p == '\0' || *p == '/' ||
								 (p[0] == '\\' && p[1] == '/')))
							{
								/*
								 * Assuming we already match 'foo/' and are at
								 * <star star slash>, just assume it matches
								 * nothing and go ahead match the rest of the
								 * pattern with the remaining string. This
								 * helps make foo/<*><*>/bar (<> because
								 * otherwise it breaks C comment syntax) match
								 * both foo/bar and foo/a/bar.
								 */
								if (p[0] == '/' &&
									dowild(p + 1, text, flags) == WM_MATCH)
									return WM_MATCH;
								match_slash = 1;
							}
							else
								return WM_ABORT_MALFORMED;
						}
						else
							/* without WM_PATHNAME, '*' == '**' */
							match_slash = (flags & WM_PATHNAME) != 0 ? 0 : 1;
						if (*p == '\0')
						{
							/* Trailing "**" matches everything.  Trailing "*" matches
							 * only if there are no more slash characters. */
							if (match_slash == 0)
							{
								if (strchr(text, '/') != null)
									return WM_NOMATCH;
							}
							return WM_MATCH;
						}
						else if (match_slash == 0 && *p == '/')
						{
							/*
							 * _one_ asterisk followed by a slash
							 * with WM_PATHNAME matches the next
							 * directory
							 */
							char* slash = strchr(text, '/');
							if (slash == null)
								return WM_NOMATCH;
							text = slash;
							/* the slash is consumed by the top-level for loop */
							break;
						}
						while (true) {
							if (t_ch == '\0')
								break;
							/*
							 * Try to advance faster when an asterisk is
							 * followed by a literal. We know in this case
							 * that the string before the literal
							 * must belong to "*".
							 * If match_slash is false, do not look past
							 * the first slash as it cannot belong to '*'.
							 */
							if (!is_glob_special(*p)) {
								p_ch = *p;
								if ((flags & WM_CASEFOLD) != 0 && ISUPPER(p_ch))
									p_ch = tolower(p_ch);
								while ((t_ch = *text) != '\0' &&
									   (match_slash != 0 || t_ch != '/')) {
									if ((flags & WM_CASEFOLD) != 0 && ISUPPER(t_ch))
										t_ch = tolower(t_ch);
									if (t_ch == p_ch)
										break;
									text++;
								}
								if (t_ch != p_ch)
									return WM_NOMATCH;
							}
							if ((matched = dowild(p, text, flags)) != WM_NOMATCH) {
								if (match_slash == 0 || matched != WM_ABORT_TO_STARSTAR)
									return matched;
							} else if (match_slash == 0 && t_ch == '/')
								return WM_ABORT_TO_STARSTAR;
							t_ch = *++text;
						}
						return WM_ABORT_ALL;
					case '[':
						p_ch = *++p;
						if (p_ch == NEGATE_CLASS2)
							p_ch = NEGATE_CLASS;
						/* Assign literal 1/0 because of "matched" comparison. */
						negated = p_ch == NEGATE_CLASS? 1 : 0;
						if (negated != 0) {
							/* Inverted character class. */
							p_ch = *++p;
						}
						prev_ch = '\0';
						matched = 0;
						// C# has no do-while with an iterator in condition statement, so using a for loop that runs at least once
						// do {
						for (bool once = true; once || (p_ch = *++p) != ']'; prev_ch = p_ch, once = false)
						{
							if (p_ch == '\0')
								return WM_ABORT_ALL;
							if (p_ch == '\\') {
								p_ch = *++p;
								if (p_ch == '\0')
									return WM_ABORT_ALL;
								if (t_ch == p_ch)
									matched = 1;
							} else if (p_ch == '-' && prev_ch != '\0' && p[1] != '\0' && p[1] != ']') {
								p_ch = *++p;
								if (p_ch == '\\') {
									p_ch = *++p;
									if (p_ch == '\0')
										return WM_ABORT_ALL;
								}
								if (t_ch <= p_ch && t_ch >= prev_ch)
									matched = 1;
								else if ((flags & WM_CASEFOLD) != 0 && ISLOWER(t_ch)) {
									uchar t_ch_upper = toupper(t_ch);
									if (t_ch_upper <= p_ch && t_ch_upper >= prev_ch)
										matched = 1;
								}
								p_ch = '\0'; /* This makes "prev_ch" get set to 0. */
							} else if (p_ch == '[' && p[1] == ':') {
								uchar* s;
								long i;
								for (s = p += 2; (p_ch = *p) != '\0' && p_ch != ']'; p++) {} /*SHARED ITERATOR*/
								if (p_ch == '\0')
									return WM_ABORT_ALL;
								i = p - s - 1;
								if (i< 0 || p[-1] != ':') {
									/* Didn't find ":]", so treat like a normal set. */
									p = s - 2;
									p_ch = '[';
									if (t_ch == p_ch)
										matched = 1;
									continue;
								}
								if (CC_EQ(s, i, "alnum")) {
									if (ISALNUM(t_ch))
										matched = 1;
								} else if (CC_EQ(s, i, "alpha")) {
									if (ISALPHA(t_ch))
										matched = 1;
								} else if (CC_EQ(s, i, "blank")) {
									if (ISBLANK(t_ch))
										matched = 1;
								} else if (CC_EQ(s, i, "cntrl")) {
									if (ISCNTRL(t_ch))
										matched = 1;
								} else if (CC_EQ(s, i, "digit")) {
									if (ISDIGIT(t_ch))
										matched = 1;
								} else if (CC_EQ(s, i, "graph")) {
									if (ISGRAPH(t_ch))
										matched = 1;
								} else if (CC_EQ(s, i, "lower")) {
									if (ISLOWER(t_ch))
										matched = 1;
								} else if (CC_EQ(s, i, "print")) {
									if (ISPRINT(t_ch))
										matched = 1;
								} else if (CC_EQ(s, i, "punct")) {
									if (ISPUNCT(t_ch))
										matched = 1;
								} else if (CC_EQ(s, i, "space")) {
									if (ISSPACE(t_ch))
										matched = 1;
								} else if (CC_EQ(s, i, "upper")) {
									if (ISUPPER(t_ch))
										matched = 1;
									else if ((flags & WM_CASEFOLD) != 0 && ISLOWER(t_ch))
										matched = 1;
								} else if (CC_EQ(s, i, "xdigit")) {
									if (ISXDIGIT(t_ch))
										matched = 1;
								} else /* malformed [:class:] string */
									return WM_ABORT_ALL;
								p_ch = '\0'; /* This makes "prev_ch" get set to 0. */
							} else if (t_ch == p_ch)
								matched = 1;
						} // while (prev_ch = p_ch, (p_ch = *++p) != ']');
						if (matched == negated ||
							((flags & WM_PATHNAME) != 0 && t_ch == '/'))
							return WM_NOMATCH;
						continue;
					}
				}

				return *text != '\0' ? WM_NOMATCH : WM_MATCH;
		}
		#endregion

		#region Helper methods to keep changes to dowild() as small as possible
		#region Aliases
		private static bool ISASCII(char c) => true;

		private static bool ISBLANK(char c) => (c == ' ' || c == '\t');

		private static bool ISGRAPH(char c) => (!char.IsControl(c) || char.IsWhiteSpace(c));

		private static bool ISPRINT(char c) => !char.IsControl(c);

		private static bool ISDIGIT(char c) => char.IsDigit(c);

		private static bool ISALNUM(char c) => char.IsLetterOrDigit(c);

		private static bool ISALPHA(char c) => char.IsLetter(c);

		private static bool ISCNTRL(char c) => char.IsControl(c);

		private static bool ISLOWER(char c) => char.IsLower(c);

		private static bool ISPUNCT(char c) => char.IsPunctuation(c) ? true : (c == '$' || c == '<' || c == '=' || c == '>' || c == '^' || c == '`' || c == '|' || c == '~'); // C has a slightly different interpretation of punctuation to Unicode classes.

		private static bool ISSPACE(char c) => char.IsWhiteSpace(c);

		private static bool ISUPPER(char c) => char.IsUpper(c);

		private static bool ISXDIGIT(char c) => char.IsDigit(c) ? true : (c > 0x40 && c < 0x47) || (c > 0x60 && c < 0x67);

		private static unsafe bool CC_EQ(char* cls, long len, string litmatch) => (len == litmatch.Length && *cls == litmatch[0] && strncmp(cls, litmatch, len) == 0);

		//private static bool sane_istest(char x, int mask) => x < 256 && ((sane_ctype[x] & (mask)) != 0); // Added size test for x to prevent IndexOutOfRangeExceptions on Unicode characters.

		//private static bool is_glob_special(char x) => sane_istest(x, GIT_GLOB_SPECIAL);

		private static bool is_glob_special(char x) => GIT_GLOB_SPECIAL.Contains(x);
		#endregion

		#region C methods
		private static char tolower(char c) => char.ToLower(c, CultureInfo.InvariantCulture);

		private static char toupper(char c) => char.ToUpper(c, CultureInfo.InvariantCulture);

		private static unsafe char* strchr(char* str, char c)
		{
			if (str == null)
			{
				return null;
			}

			char* returnValue = str;
			while (true)
			{
				if (*returnValue == c)
				{
					return returnValue;
				}
				if (*returnValue == '\0')
				{
					break;
				}
				returnValue++;
			}

			return null;
		}

		private static unsafe int strncmp(char* str1, string str2, long len)
		{
			char* temp = str1;
			int j;
			for (int i = 0; i < len; i++, temp++)
			{
				if ((j = (*temp).CompareTo(str2[i])) != 0)
				{
					return j;
				}
			}
			return 0;
		}
		#endregion
		#endregion
		#endregion
	}
}
