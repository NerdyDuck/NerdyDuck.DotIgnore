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
using System.IO;

namespace NerdyDuck.DotIgnore;

/// <summary>
/// Represents a list of <see cref="ExcludePattern"/>s.
/// </summary>
public class ExcludeList : List<ExcludePattern>
{
	/// <summary>
	/// Gets or sets a string describing the source of the <see cref="ExcludeList"/>.
	/// </summary>
	/// <remarks>For git, this is the path of the <c>.gitignore</c> file the list was parsed from. If the list is from another source, the property should be <see cref="string.Empty"/>.</remarks>
	public string Source
	{
		get; set;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ExcludeList"/> class.
	/// </summary>
	/// <remarks><see cref="Source"/> is set to <see cref="string.Empty"/>.</remarks>
	public ExcludeList()
		: base()
	{
		Source = string.Empty;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ExcludeList"/> class with the specified source.
	/// </summary>
	/// <param name="source">A string describing the source of the <see cref="ExcludeList"/>.</param>
	public ExcludeList(string source)
		: base()
	{
		Source = source ?? throw new ArgumentNullException(nameof(source));
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ExcludeList"/> class with the specified exclude patterns.
	/// </summary>
	/// <param name="patterns">A list of <see cref="ExcludePattern"/>s.</param>
	/// <remarks><see cref="Source"/> is set to <see cref="string.Empty"/>.</remarks>
	public ExcludeList(IEnumerable<ExcludePattern> patterns)
		: base(patterns)
	{
		Source = string.Empty;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ExcludeList"/> class with the specified source and exclude patterns.
	/// </summary>
	/// <param name="source">A string describing the source of the <see cref="ExcludeList"/>.</param>
	/// <param name="patterns">A list of <see cref="ExcludePattern"/>s.</param>
	public ExcludeList(string source, IEnumerable<ExcludePattern> patterns)
		: base(patterns) => Source = source ?? throw new ArgumentNullException(nameof(source));

	/// <summary>
	/// Parses a <see cref="ExcludeList"/> from the specified <c>.gitignore</c>-style file.
	/// </summary>
	/// <param name="path">The path to a file with <c>.gitignore</c>-formatted content.</param>
	/// <returns>A <see cref="ExcludeList"/>.</returns>
	public static ExcludeList FromFile(string path) => FromFile(path, 0);

	/// <summary>
	/// Parses a <see cref="ExcludeList"/> from the specified <c>.gitignore</c>-style file.
	/// </summary>
	/// <param name="path">The path to a file with <c>.gitignore</c>-formatted content.</param>
	/// <param name="rankGroup">A value defining a group of rankings that the <see cref="ExcludePattern"/>s read from the file> are a part of.</param>
	/// <returns>A <see cref="ExcludeList"/>.</returns>
	public static ExcludeList FromFile(string path, int rankGroup)
	{
		if (string.IsNullOrWhiteSpace(path))
		{
			throw new ArgumentException(TextResources.ExcludeList_FromFile_PathEmpty, nameof(path));
		}
		if (!File.Exists(path))
		{
			throw new FileNotFoundException(TextResources.ExcludeList_FromFile_FileNotFound, path);
		}

		using FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
		ExcludeList returnValue = FromStream(stream, rankGroup);
		returnValue.Source = path;
		return returnValue;
	}

	/// <summary>
	/// Parses a <see cref="ExcludeList"/> from the specified stream containing <c>.gitignore</c>-formatted data.
	/// </summary>
	/// <param name="stream">The stream with <c>.gitignore</c>-formatted content.</param>
	/// <returns>A <see cref="ExcludeList"/>.</returns>
	public static ExcludeList FromStream(Stream stream) => FromStream(stream, 0);

	/// <summary>
	/// Parses a <see cref="ExcludeList"/> from the specified stream containing <c>.gitignore</c>-formatted data.
	/// </summary>
	/// <param name="stream">The stream with <c>.gitignore</c>-formatted content.</param>
	/// <param name="rankGroup">A value defining a group of rankings that the <see cref="ExcludePattern"/>s read from the <paramref name="stream"/> are a part of.</param>
	/// <returns>A <see cref="ExcludeList"/>.</returns>
	public static ExcludeList FromStream(Stream stream, int rankGroup)
	{
		if (stream == null)
		{
			throw new ArgumentNullException(nameof(stream));
		}
		if (!stream.CanRead)
		{
			throw new ArgumentException(TextResources.ExcludeList_FromStream_NotReadable, nameof(stream));
		}

		using StreamReader reader = new StreamReader(stream);
		return FromReader(reader, rankGroup);
	}

	/// <summary>
	/// Parses a <see cref="ExcludeList"/> from the specified <see cref="TextReader"/> containing <c>.gitignore</c>-formatted data.
	/// </summary>
	/// <param name="reader">The <see cref="TextReader"/> with <c>.gitignore</c>-formatted content.</param>
	/// <returns>A <see cref="ExcludeList"/>.</returns>
	public static ExcludeList FromReader(TextReader reader) => FromReader(reader, 0);

	/// <summary>
	/// Parses a <see cref="ExcludeList"/> from the specified <see cref="TextReader"/> containing <c>.gitignore</c>-formatted data.
	/// </summary>
	/// <param name="reader">The <see cref="TextReader"/> with <c>.gitignore</c>-formatted content.</param>
	/// <param name="rankGroup">A value defining a group of rankings that the <see cref="ExcludePattern"/>s read from the <paramref name="reader"/> are a part of.</param>
	/// <returns>A <see cref="ExcludeList"/>.</returns>
	public static ExcludeList FromReader(TextReader reader, int rankGroup)
	{
		if (reader == null)
		{
			throw new ArgumentNullException(nameof(reader));
		}

		ExcludeList returnValue = new ExcludeList();
		string line;
		int lineCount = 0;
		while ((line = reader.ReadLine()) != null)
		{
			lineCount++;
			line = line.Trim();
			if (string.IsNullOrEmpty(line))
			{
				continue;
			}
			if (line[0] == '#')
			{
				continue;
			}
			returnValue.Add(new ExcludePattern(line, lineCount, rankGroup));
		}

		return returnValue;
	}

	/// <summary>
	/// Parses a <see cref="ExcludeList"/> from the specified string containing <c>.gitignore</c>-formatted data.
	/// </summary>
	/// <param name="excludes">The string with <c>.gitignore</c>-formatted content.</param>
	/// <returns>A <see cref="ExcludeList"/>.</returns>
	public static ExcludeList FromString(string excludes) => FromString(excludes, 0);

	/// <summary>
	/// Parses a <see cref="ExcludeList"/> from the specified string containing <c>.gitignore</c>-formatted data.
	/// </summary>
	/// <param name="excludes">The string with <c>.gitignore</c>-formatted content.</param>
	/// <param name="rankGroup">A value defining a group of rankings that the <see cref="ExcludePattern"/>s read from the string are a part of.</param>
	/// <returns>A <see cref="ExcludeList"/>.</returns>
	public static ExcludeList FromString(string excludes, int rankGroup)
	{
		if (excludes == null)
		{
			throw new ArgumentNullException(nameof(excludes));
		}

		using StringReader reader = new StringReader(excludes);
		return FromReader(reader, rankGroup);
	}
}
