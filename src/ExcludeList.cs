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
using System.IO;
using System.Text;

namespace NerdyDuck.DotIgnore
{
	/// <summary>
	/// Represents a list of <see cref="ExcludePattern"/>s.
	/// </summary>
	public class ExcludeList : List<ExcludePattern>
	{
		#region Properties
		/// <summary>
		/// Gets or sets a string describing the source of the <see cref="ExcludeList"/>.
		/// </summary>
		/// <remarks>For git, this is the path of the <c>.gitignore</c> file the list was parsed from. If the list is from another source, the property should be <see cref="string.Empty"/>.</remarks>
		public string Source
		{
			get; set;
		}
		#endregion

		#region Constructors
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
			: base(patterns)
		{
			Source = source ?? throw new ArgumentNullException(nameof(source));
		}
		#endregion

		#region Public methods
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

			using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				ExcludeList returnValue = FromStream(stream, rankGroup);
				returnValue.Source = path;
				return returnValue;
			}
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

			using (StreamReader reader = new StreamReader(stream))
			{
				return FromReader(reader, rankGroup);
			}
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

			using (StringReader reader = new StringReader(excludes))
			{
				return FromReader(reader, rankGroup);
			}
		}
		#endregion
	}
}
