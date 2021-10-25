﻿#region Copyright
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
using System.IO;
using System.Text;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;

namespace NerdyDuck.DotIgnore
{
	/// <summary>
	/// Searches the file system for files with names that are not ignored by the specified patterns.
	/// </summary>
	/// <remarks>The patterns used follow the syntax of .gitignore files. See https://git-scm.com/docs/gitignore for more information.</remarks>
	public class Matcher
	{
		#region Private fields
		private StringComparison _comparisonType;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Matcher"/> class using case-sensitive matching.
		/// </summary>
		public Matcher()
			: this(StringComparison.Ordinal)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Matcher"/> class using the string comparison method specified.
		/// </summary>
		/// <param name="comparisonType">The <see cref="StringComparison"/> to use.</param>
		public Matcher(StringComparison comparisonType)
		{
			_comparisonType = comparisonType;
		}
		#endregion

		#region Public methods
		#region AddIgnore
		/// <summary>
		/// Add a name pattern for files and directories the matcher should exclude from the results. Patterns are relative to the root directory given when <see cref="Execute(DirectoryInfoBase)"/> is called. 
		/// </summary>
		/// <param name="pattern">The ignore pattern.</param>
		/// <returns>The current <see cref="Matcher"/> instance.</returns>
		public Matcher AddIgnore(string pattern)
		{
			// TODO
			throw new NotImplementedException();
		}
		#endregion

		#region Execute
		/// <summary>
		/// Searches the directory specified for all files not matching the ignore patterns added to this instance of <see cref="Matcher"/>.
		/// </summary>
		/// <param name="directoryInfo">The root directory for the search.</param>
		/// <returns>Always returns an instance of <see cref="Microsoft.Extensions.FileSystemGlobbing.PatternMatchingResult"/>, even if no files were found that were not ignored.</returns>
		[CLSCompliant(false)]
		public Microsoft.Extensions.FileSystemGlobbing.PatternMatchingResult Execute(DirectoryInfoBase directoryInfo)
		{
			if (directoryInfo == null)
			{
				throw new ArgumentNullException(nameof(directoryInfo));
			}

			// TODO
			throw new NotImplementedException();
		}
		#endregion

		#region Load
		/// <summary>
		/// Loads a list of ignore patterns from the specified file.
		/// </summary>
		/// <param name="path">A relative or absolute path for the file to read.</param>
		/// <returns>The current <see cref="Matcher"/> instance.</returns>
		/// <remarks>The file must have the syntax of a .gitignore file. See https://git-scm.com/docs/gitignore for more information.</remarks>
		public Matcher Load(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException(nameof(path));
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(TextResources.Matcher_Load_PathEmpty, nameof(path));
			}

			// TODO
			throw new NotImplementedException();
		}

		/// <summary>
		/// Loads a list of ignore patterns from the specified <see cref="TextReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="TextReader"/> containing the ignore patterns to read.</param>
		/// <returns>The current <see cref="Matcher"/> instance.</returns>
		/// <remarks>The data provided by the <see cref="TextReader"/> must have the syntax of a .gitignore file. See https://git-scm.com/docs/gitignore for more information.</remarks>
		public Matcher Load(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException(nameof(reader));
			}

			// TODO
			throw new NotImplementedException();
		}

		/// <summary>
		/// Loads a list of ignore patterns from the specified stream.
		/// </summary>
		/// <param name="stream">The stream containing the ignore patterns to read.</param>
		/// <returns>The current <see cref="Matcher"/> instance.</returns>
		/// <remarks>The data provided by the stream must have the syntax of a .gitignore file. See https://git-scm.com/docs/gitignore for more information.
		/// <see cref="UTF8Encoding"/> is used to read the stream.</remarks>
		public Matcher Load(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException(nameof(stream));
			}

			// TODO
			throw new NotImplementedException();
		}

		/// <summary>
		/// Loads a list of ignore patterns from the specified stream, with the specified character encoding.
		/// </summary>
		/// <param name="stream">The stream containing the ignore patterns to read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <returns>The current <see cref="Matcher"/> instance.</returns>
		/// <remarks>The data provided by the stream must have the syntax of a .gitignore file. See https://git-scm.com/docs/gitignore for more information.</remarks>
		public Matcher Load(Stream stream, Encoding encoding)
		{
			if (stream == null)
			{
				throw new ArgumentNullException(nameof(stream));
			}
			if (encoding == null)
			{
				throw new ArgumentNullException(nameof(encoding));
			}

			// TODO
			throw new NotImplementedException();
		}
		#endregion

		#region LoadString
		/// <summary>
		/// Loads a list of ignore patterns from the specified string.
		/// </summary>
		/// <param name="patterns">The string containing the ignore patterns to read.</param>
		/// <returns>The current <see cref="Matcher"/> instance.</returns>
		/// <remarks>The text contained in the string must have the syntax of a .gitignore file. See https://git-scm.com/docs/gitignore for more information.</remarks>
		public Matcher LoadString(string patterns)
		{
			if (patterns == null)
			{
				throw new ArgumentNullException(nameof(patterns));
			}
			if (patterns.Length == 0)
			{
				throw new ArgumentException(TextResources.Matcher_Load_StringEmpty, nameof(patterns));
			}

			// TODO
			throw new NotImplementedException();
		}
		#endregion

		#region Merge
		/// <summary>
		/// Merges the ignore patterns of two <see cref="Matcher"/> instances into a new instance.
		/// </summary>
		/// <param name="matcher1">A <see cref="Matcher"/>.</param>
		/// <param name="matcher2">A <see cref="Matcher"/>.</param>
		/// <returns>A new instance of <see cref="Matcher"/> containing the ignore patterns of the provides <see cref="Matcher"/>s.</returns>
		public static Matcher Merge(Matcher matcher1, Matcher matcher2)
		{
			if (matcher1 == null)
			{
				throw new ArgumentNullException(nameof(matcher1));
			}
			if (matcher2 == null)
			{
				throw new ArgumentNullException(nameof(matcher2));
			}

			if (matcher1._comparisonType != matcher2._comparisonType)
			{
				throw new ArgumentException(TextResources.Matcher_Merge_InvalidComparison);
			}

			// TODO
			throw new NotImplementedException();
		}
		#endregion
		#endregion

		#region Private methods
		#endregion
	}
}