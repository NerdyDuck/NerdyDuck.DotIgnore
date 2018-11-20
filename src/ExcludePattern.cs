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
 ******************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace NerdyDuck.DotIgnore
{
	/// <summary>
	/// Specifies a .gitignore-style exclude pattern.
	/// </summary>
	public class ExcludePattern
	{
		#region Private fields
		#endregion

		#region Properties
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
		#endregion

		#region Constructors
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
			Pattern = (string.IsNullOrWhiteSpace(pattern)) ? throw new ArgumentException(TextResources.ExcludePattern_ctor_PatternEmpty, nameof(pattern)) : pattern;
			Rank = rank;
			RankGroup = rankGroup;
			ExaminePattern();
		}
		#endregion

		#region Private methods
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
		#endregion
	}
}
