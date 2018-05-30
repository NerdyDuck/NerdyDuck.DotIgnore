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

namespace NerdyDuck.DotIgnore
{
	/// <summary>
	/// Extension methods for the <see cref="Matcher"/> and <see cref="Microsoft.Extensions.FileSystemGlobbing.Matcher"/> classes.
	/// </summary>
	public static class MatcherExtensions
	{
		#region AddIgnorePatterns
		/// <summary>
		/// Adds multiple ignore patterns to the <see cref="Matcher"/>.
		/// </summary>
		/// <param name="matcher">The matcher to which the ignore patterns are added.</param>
		/// <param name="ignorePatternsGroups">A list of ignore patterns.</param>
		/// <returns>The current <see cref="Matcher"/> instance.</returns>
		public static Matcher AddIgnorePatterns(this Matcher matcher, params IEnumerable<string>[] ignorePatternsGroups)
		{
			if (matcher == null)
			{
				throw new ArgumentNullException(nameof(matcher));
			}

			// TODO
			throw new NotImplementedException();
		}
		#endregion

		#region GetResultsInFullPath
		/// <summary>
		/// Searches the specified directory for all files not ignored by the patterns added to this instance of <see cref="Matcher"/>.
		/// </summary>
		/// <param name="matcher">The matcher to use for filtering.</param>
		/// <param name="directoryPath">The root directory for the search.</param>
		/// <returns>A list of the absolute paths of all files that were not ignored; an empty enumerable if not files were found.</returns>
		public static IEnumerable<string> GetResultsInFullPath(this Matcher matcher, string directoryPath)
		{
			if (matcher == null)
			{
				throw new ArgumentNullException(nameof(matcher));
			}

			// TODO
			throw new NotImplementedException();
		}
		#endregion

		#region Match
		/// <summary>
		/// Matches the specified files with the ignore patterns in the matcher without going to disk.
		/// </summary>
		/// <param name="matcher">The matcher that holds the ignore patterns and a pattern matching type.</param>
		/// <param name="files">The files to run the matcher against.</param>
		/// <returns>Always returns an instance of <see cref="Microsoft.Extensions.FileSystemGlobbing.PatternMatchingResult"/>, even if no files were found that were not ignored.</returns>
		public static Microsoft.Extensions.FileSystemGlobbing.PatternMatchingResult Match(this Matcher matcher, IEnumerable<string> files)
		{
			if (matcher == null)
			{
				throw new ArgumentNullException(nameof(matcher));
			}

			// TODO
			throw new NotImplementedException();
		}

		/// <summary>
		/// Matches the specified file with the ignore patterns in the matcher without going to disk.
		/// </summary>
		/// <param name="matcher">The matcher that holds the ignore patterns and a pattern matching type.</param>
		/// <param name="file">The file to run the matcher against.</param>
		/// <returns>Always returns an instance of <see cref="Microsoft.Extensions.FileSystemGlobbing.PatternMatchingResult"/>, even if no files were found that were not ignored.</returns>
		public static Microsoft.Extensions.FileSystemGlobbing.PatternMatchingResult Match(this Matcher matcher, string file)
		{
			if (matcher == null)
			{
				throw new ArgumentNullException(nameof(matcher));
			}

			// TODO
			throw new NotImplementedException();
		}

		/// <summary>
		/// Matches the specified files with the ignore patterns in the matcher without going to disk.
		/// </summary>
		/// <param name="matcher">The matcher that holds the ignore patterns and a pattern matching type.</param>
		/// <param name="rootDir">The root directory for the matcher to match the files from.</param>
		/// <param name="files">The files to run the matcher against.</param>
		/// <returns>Always returns an instance of <see cref="Microsoft.Extensions.FileSystemGlobbing.PatternMatchingResult"/>, even if no files were found that were not ignored.</returns>
		public static Microsoft.Extensions.FileSystemGlobbing.PatternMatchingResult Match(this Matcher matcher, string rootDir, IEnumerable<string> files)
		{
			if (matcher == null)
			{
				throw new ArgumentNullException(nameof(matcher));
			}

			// TODO
			throw new NotImplementedException();
		}

		/// <summary>
		/// Matches the specified file with the ignore patterns in the matcher without going to disk.
		/// </summary>
		/// <param name="matcher">The matcher that holds the ignore patterns and a pattern matching type.</param>
		/// <param name="rootDir">The root directory for the matcher to match the file from.</param>
		/// <param name="file">The file to run the matcher against.</param>
		/// <returns>Always returns an instance of <see cref="Microsoft.Extensions.FileSystemGlobbing.PatternMatchingResult"/>, even if no files were found that were not ignored.</returns>
		public static Microsoft.Extensions.FileSystemGlobbing.PatternMatchingResult Match(this Matcher matcher, string rootDir, string file)
		{
			if (matcher == null)
			{
				throw new ArgumentNullException(nameof(matcher));
			}

			// TODO
			throw new NotImplementedException();
		}
		#endregion

		#region IsMatch(DotIgnore.Matcher)
		/// <summary>
		/// Checks if the specified file does not match any of the ignore patterns in the current matcher.
		/// </summary>
		/// <param name="matcher">The matcher that holds the ignore patterns and a pattern matching type.</param>
		/// <param name="file">The file to run the matcher against.</param>
		/// <returns><see langword="true"/>, if the file does not match any of the ignore patterns; otherwise, <see langword="false"/>.</returns>
		public static bool IsMatch(this Matcher matcher, string file)
		{
			if (matcher == null)
			{
				throw new ArgumentNullException(nameof(matcher));
			}

			// TODO
			throw new NotImplementedException();
		}

		/// <summary>
		/// Checks if the specified file does not match any of the ignore patterns in the current matcher.
		/// </summary>
		/// <param name="matcher">The matcher that holds the ignore patterns and a pattern matching type.</param>
		/// <param name="rootDir">The root directory for the matcher to match the file from.</param>
		/// <param name="file">The file to run the matcher against.</param>
		/// <returns><see langword="true"/>, if the file does not match any of the ignore patterns; otherwise, <see langword="false"/>.</returns>
		public static bool IsMatch(this Matcher matcher, string rootDir, string file)
		{
			if (matcher == null)
			{
				throw new ArgumentNullException(nameof(matcher));
			}

			// TODO
			throw new NotImplementedException();
		}
		#endregion

		#region IsMatch(FileSystemGlobbing.Matcher)
		/// <summary>
		/// Checks if the specified file matches any of the patterns in the current matcher.
		/// </summary>
		/// <param name="matcher">The matcher that holds the patterns and a pattern matching type.</param>
		/// <param name="file">The file to run the matcher against.</param>
		/// <returns><see langword="true"/>, if the file does matches any of the patterns; otherwise, <see langword="false"/>.</returns>
		public static bool IsMatch(this Microsoft.Extensions.FileSystemGlobbing.Matcher matcher, string file)
		{
			if (matcher == null)
			{
				throw new ArgumentNullException(nameof(matcher));
			}

			// TODO
			throw new NotImplementedException();
		}

		/// <summary>
		/// Checks if the specified file matches any of the patterns in the current matcher.
		/// </summary>
		/// <param name="matcher">The matcher that holds the patterns and a pattern matching type.</param>
		/// <param name="rootDir">The root directory for the matcher to match the file from.</param>
		/// <param name="file">The file to run the matcher against.</param>
		/// <returns><see langword="true"/>, if the file does matches any of the patterns; otherwise, <see langword="false"/>.</returns>
		public static bool IsMatch(this Microsoft.Extensions.FileSystemGlobbing.Matcher matcher, string rootDir, string file)
		{
			if (matcher == null)
			{
				throw new ArgumentNullException(nameof(matcher));
			}

			// TODO
			throw new NotImplementedException();
		}
		#endregion
	}
}
