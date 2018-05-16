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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NerdyDuck.DotIgnore.FileSystem.Physical
{
	/// <summary>
	/// Represents the contents of a directory with a local or UNC path.
	/// </summary>
	public class PhysicalDirectoryContentCollection : IDirectoryContentCollection
	{
		#region Private fields
		private IEnumerable<IFileSystemObject> _entries;
		private readonly string _directoryPath;
		private readonly int _rootPathLength;
		#endregion

		#region Properties
		/// <summary>
		/// Gets a value indicating whether the directory exists.
		/// </summary>
		/// <value><see langword="true"/>, if the directory exists; otherwise, <see langword="false"/>.</value>
		public bool Exists => Directory.Exists(_directoryPath);
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="PhysicalDirectoryContentCollection"/> class with the specified directory path and root path length.
		/// </summary>
		/// <param name="directoryPath">The full path of the directory to get the contents from.</param>
		/// <param name="rootPathLength">The length of the root part of <paramref name="directoryPath"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="directoryPath"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="rootPathLength"/> is negative.</exception>
		public PhysicalDirectoryContentCollection(string directoryPath, int rootPathLength)
		{
			_directoryPath = directoryPath ?? throw new ArgumentNullException(nameof(directoryPath));
			_rootPathLength = (rootPathLength < 0) ? throw new ArgumentOutOfRangeException(nameof(rootPathLength), rootPathLength, TextResources.PhysicalDirectoryContents_ctor_RootLengthNegative) : rootPathLength;
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Returns an enumerator that iterates through the <see cref="IFileSystemObject"/>s in the <see cref="IDirectoryContentCollection"/>.
		/// </summary>
		/// <returns>An instance of <see cref="IEnumerator{T}"/>.</returns>
		public IEnumerator<IFileSystemObject> GetEnumerator()
		{
			EnsureInitialized();
			return _entries.GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator that iterates through the <see cref="IFileSystemObject"/>s in the <see cref="IDirectoryContentCollection"/>.
		/// </summary>
		/// <returns>An instance of <see cref="IEnumerator"/>.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			EnsureInitialized();
			return _entries.GetEnumerator();
		}
		#endregion

		#region Private methods
		private void EnsureInitialized()
		{
			try
			{
				_entries = new DirectoryInfo(_directoryPath)
					.EnumerateFileSystemInfos()
					.Select<FileSystemInfo, IFileSystemObject>(info =>
					{
						if (info is FileInfo file)
						{
							return new PhysicalFile(file, info.FullName.Substring(_rootPathLength));
						}
						else if (info is DirectoryInfo dir)
						{
							return new PhysicalDirectory(dir, info.FullName.Substring(_rootPathLength));
						}

						// Shouldn't happen unless BCL introduces new implementation of base type
						throw new InvalidOperationException(TextResources.PhysicalDirectoryContents_EnsureInitialized_InvalidType);
					});
			}
			catch (Exception ex) when (ex is DirectoryNotFoundException || ex is IOException)
			{
				_entries = Enumerable.Empty<IFileSystemObject>();
			}
		}
		#endregion
	}
}
