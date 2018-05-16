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
using System.IO;
using System.Security;

namespace NerdyDuck.DotIgnore.FileSystem.Physical
{
	/// <summary>
	/// Represents a file on a physical file system (local or UNC paths).
	/// </summary>
	public class PhysicalFile : IFileSystemObject
	{
		#region Private fields
		private readonly FileInfo _fileInfo;
		private readonly string _relativePath;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the date and time when the file was created.
		/// </summary>
		/// <value>The creation date and time of the current <see cref="PhysicalFile"/>.</value>
		/// <exception cref="InvalidOperationException">The file does not exist.</exception>
		/// <exception cref="IOException">File information cannot be accessed.</exception>
		public DateTimeOffset Created => _fileInfo.Exists ? _fileInfo.CreationTime : throw FileDoesNotExistException();

		/// <summary>
		/// Gets a value indicating whether the file represented by the current <see cref="PhysicalFile"/> exists.
		/// </summary>
		/// <value><see langword="true"/>, if the file exists; otherwise, <see langword="false"/>.</value>
		public bool Exists => _fileInfo.Exists;

		/// <summary>
		/// Gets a value indicating whether the current <see cref="IFileSystemObject"/> is a file or a directory.
		/// </summary>
		/// <value>Always returns <see langword="false"/>.</value>
		public bool IsDirectory => false;

		/// <summary>
		/// Gets the date and time when the file was last modified.
		/// </summary>
		/// <value>The modification date and time of the current <see cref="PhysicalFile"/>.</value>
		/// <exception cref="InvalidOperationException">The file does not exist.</exception>
		/// <exception cref="IOException">File information cannot be accessed.</exception>
		public DateTimeOffset LastModified => _fileInfo.Exists ? _fileInfo.LastWriteTime : throw FileDoesNotExistException();

		/// <summary>
		/// Gets the length of the current <see cref="PhysicalFile"/>.
		/// </summary>
		/// <value>The length of the file, in bytes.</value>
		/// <exception cref="InvalidOperationException">The file does not exist.</exception>
		/// <exception cref="IOException">File information cannot be accessed.</exception>
		public long Length => _fileInfo.Exists ? _fileInfo.Length : throw FileDoesNotExistException();

		/// <summary>
		/// When implemented, gets the name of the file, not including any path.
		/// </summary>
		/// <value>The name of the <see cref="PhysicalFile"/>.</value>
		public string Name => _fileInfo.Name;

		/// <summary>
		/// When implemented, gets the path to the file, including the file name.
		/// </summary>
		/// <value>The full path to the file.</value>
		public string PhysicalPath => _fileInfo.FullName;

		/// <summary>
		/// Gets the path to the file or directory, relative to the <see cref="IFileSystemProvider.Root"/>.
		/// </summary>
		/// <value>The relative path to the file or directory.</value>
		public string RelativePath => _relativePath;
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="PhysicalFile"/> class with the specified file information, and the relative path of the file.
		/// </summary>
		/// <param name="fileInfo">The underlying <see cref="FileInfo"/>, provides by the physical file system.</param>
		/// <param name="relativePath">The path to the file, relative to the <see cref="IFileSystemProvider.Root"/></param>
		/// <exception cref="ArgumentNullException"><paramref name="fileInfo"/> is <see langword="null"/>.</exception>
		public PhysicalFile(FileInfo fileInfo, string relativePath)
		{
			_fileInfo = fileInfo ?? throw new ArgumentNullException(nameof(fileInfo));
			_relativePath = relativePath;
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Creates an <see cref="InvalidOperationException"/> with a message explaining that the file does not exist.
		/// </summary>
		/// <returns>An <see cref="InvalidOperationException"/>.</returns>
		private static InvalidOperationException FileDoesNotExistException() => new InvalidOperationException(TextResources.Global_FileDoesNotExist);
		#endregion
	}
}
