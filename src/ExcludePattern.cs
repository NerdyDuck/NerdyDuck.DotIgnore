#region Copyright
/*******************************************************************************
 * NerdyDuck.DotIgnore - A library that implements a file filter using the
 * patterns of .gitignore.
 * 
 * The MIT License (MIT)
 *
 * Copyright (c) Daniel Kopp, dak@nerdyduck.de
 *
 * All rights reserved.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 ******************************************************************************/
#endregion

namespace NerdyDuck.DotIgnore;

/// <summary>
/// Specifies a .gitignore-style exclude pattern.
/// </summary>
public class ExcludePattern
{
	/// <summary>
	/// Gets the exclude pattern.
	/// </summary>
	public string Pattern
	{
		get; private set;
	}

	/// <summary>
	/// Gets a value specifying the handling of the pattern.
	/// </summary>
	public ExcludePatternFlags PatternFlags
	{
		get; private set;
	}

	/// <summary>
	/// Gets the number of characters from the start of <see cref="Pattern"/> until the first wildcard character.
	/// </summary>
	public int NoWildcardLength
	{
		get; private set;
	}

	/// <summary>
	/// Gets a value indicating if the <see cref="Pattern"/> contains any wildcard characters.
	/// </summary>
	public bool ContainsWildcards => NoWildcardLength < Pattern.Length;

	/// <summary>
	/// Gets or sets a rank used to compute the order of the pattern in a group of patterns.
	/// </summary>
	/// <value>An integer value. The smaller the value, the higher the rank.</value>
	/// <remarks>This is usually the line number of the pattern in the <c>.gitignore</c> file.</remarks>
	public int Rank
	{
		get; set;
	}

	/// <summary>
	/// Gets or sets a value defining a group of rankings (in conjunction with <see cref="Rank"/> that the <see cref="ExcludePattern"/> is a part of.
	/// </summary>
	/// <value>An integer value. The smaller the value, the higher the rank group.</value>
	/// <remarks>For git, this defines the origin of the <see cref="ExcludePattern"/>, e.g. from a <c>.gitginore</c> file, a fallback like <c>.git/info/exclude</c> or <c>core.excludesfile</c>, or command line parameters.</remarks>
	public int RankGroup
	{
		get; set;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ExcludePattern"/> class with the specified pattern and flags.
	/// </summary>
	/// <param name="pattern">The exclude pattern.</param>
	/// <remarks><see cref="PatternFlags"/> are set according to the specified <paramref name="pattern"/>. <see cref="Rank"/> and <see cref="RankGroup"/> are set to 0.</remarks>
	public ExcludePattern(string pattern)
		: this(pattern, 0, 0)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ExcludePattern"/> class with the specified pattern, flags, rank and rank group.
	/// </summary>
	/// <param name="pattern">The exclude pattern.</param>
	/// <param name="rank">A rank used to compute the order of the pattern in a group of patterns.</param>
	/// <param name="rankGroup">A value defining a group of rankings (in conjunction with <paramref name="rank"/> that the <see cref="ExcludePattern"/> is a part of.</param>
	/// <remarks><see cref="PatternFlags"/> are set according to the specified <paramref name="pattern"/>.</remarks>
	public ExcludePattern(string pattern, int rank, int rankGroup)
	{
		Pattern = string.IsNullOrWhiteSpace(pattern) ? throw new ArgumentException(TextResources.ExcludePattern_ctor_PatternEmpty, nameof(pattern)) : pattern;
		Rank = rank;
		RankGroup = rankGroup;
		ExaminePattern();
	}

	private void ExaminePattern()
	{
		int startIndex = 0;
		int endIndex = Pattern.Length - 1;
		if (Pattern[0] == '#')
		{
			throw new FormatException(TextResources.ExcludePattern_ExaminePattern_Comment);
		}

		if (Pattern[startIndex] == '!')
		{
			PatternFlags |= ExcludePatternFlags.Negation;
			startIndex++;
		}

		if (Pattern[endIndex] == '/')
		{
			PatternFlags |= ExcludePatternFlags.MustBeDirectory;
			endIndex--;
		}

		if (PatternFlags != ExcludePatternFlags.None)
		{
			Pattern = Pattern.Substring(startIndex, endIndex - startIndex + 1);
		}

		if (Pattern.IndexOf('/') < 0)
		{
			PatternFlags |= ExcludePatternFlags.NoDirectory;
		}

		NoWildcardLength = Pattern.IndexOfAny(Native.WildMatch.GIT_GLOB_SPECIAL);
		if (NoWildcardLength < 0)
		{
			NoWildcardLength = Pattern.Length;
		}

		if (Pattern[0] == '*' && Pattern.IndexOfAny(Native.WildMatch.GIT_GLOB_SPECIAL, 1) < 0)
		{
			PatternFlags |= ExcludePatternFlags.EndsWith;
		}
	}
}
