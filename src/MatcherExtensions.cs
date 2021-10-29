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

using System.Collections.Generic;

namespace NerdyDuck.DotIgnore;

/// <summary>
/// Extension methods for the <see cref="Matcher"/> and <see cref="Microsoft.Extensions.FileSystemGlobbing.Matcher"/> classes.
/// </summary>
public static class MatcherExtensions
{
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
}
