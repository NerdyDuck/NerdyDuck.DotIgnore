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

namespace NerdyDuck.DotIgnore.FileSystem.Physical
{
	/// <summary>
	/// Represents a directory on a physical file system (local or UNC paths).
	/// </summary>
	public class PhysicalDirectory : IFileSystemObject
	{
		#region Private fields
		private readonly DirectoryInfo _directoryInfo;
		private readonly string _relativePath;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the date and time when the directory was created.
		/// </summary>
		/// <value>The creation date and time of the current <see cref="PhysicalDirectory"/>.</value>
		/// <exception cref="InvalidOperationException">The directory does not exist.</exception>
		/// <exception cref="IOException">Directory information cannot be accessed.</exception>
		public DateTimeOffset Created => _directoryInfo.Exists ? _directoryInfo.CreationTime : throw DirectoryDoesNotExistException();

		/// <summary>
		/// Gets a value indicating whether the directory represented by the current <see cref="PhysicalDirectory"/> exists.
		/// </summary>
		/// <value><see langword="true"/>, if the directory exists; otherwise, <see langword="false"/>.</value>
		public bool Exists => _directoryInfo.Exists;

		/// <summary>
		/// Gets a value indicating whether the current <see cref="IFileSystemObject"/> is a file or a directory.
		/// </summary>
		/// <value>Always returns <see langword="true"/>.</value>
		public bool IsDirectory => true;

		/// <summary>
		/// Gets the date and time when the directory was last modified.
		/// </summary>
		/// <value>The modification date and time of the current <see cref="PhysicalDirectory"/>.</value>
		/// <exception cref="InvalidOperationException">The directory does not exist.</exception>
		/// <exception cref="IOException">Directory information cannot be accessed.</exception>
		public DateTimeOffset LastModified => _directoryInfo.Exists ? _directoryInfo.LastWriteTime : throw DirectoryDoesNotExistException();

		/// <summary>
		/// Gets the length of the current <see cref="PhysicalDirectory"/>.
		/// </summary>
		/// <value>Always returns 0.</value>
		public long Length => 0;

		/// <summary>
		/// Gets the name of the directory, not including any path.
		/// </summary>
		/// <value>The name of the <see cref="PhysicalDirectory"/>.</value>
		public string Name => _directoryInfo.Name;

		/// <summary>
		/// Gets the path to the directory.
		/// </summary>
		/// <value>The full path to the directory.</value>
		public string PhysicalPath => _directoryInfo.FullName;

		/// <summary>
		/// Gets the path to the file or directory, relative to the <see cref="IFileSystemProvider.Root"/>.
		/// </summary>
		/// <value>The relative path to the file or directory.</value>
		public string RelativePath => _relativePath;
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="PhysicalDirectory"/> class with the specified directory information and relative path to the directory.
		/// </summary>
		/// <param name="directoryInfo">The underlying <see cref="DirectoryInfo"/>, provides by the physical file system.</param>
		/// <param name="relativePath">The path to the directory, relative to the <see cref="IFileSystemProvider.Root"/></param>
		/// <exception cref="ArgumentNullException"><paramref name="directoryInfo"/> is <see langword="null"/>.</exception>
		public PhysicalDirectory(DirectoryInfo directoryInfo, string relativePath)
		{
			_directoryInfo = directoryInfo ?? throw new ArgumentNullException(nameof(directoryInfo));
			_relativePath = relativePath;
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Creates an <see cref="InvalidOperationException"/> with a message explaining that the directory does not exist.
		/// </summary>
		/// <returns>An <see cref="InvalidOperationException"/>.</returns>
		private static InvalidOperationException DirectoryDoesNotExistException() => new InvalidOperationException(TextResources.Global_DirectoryDoesNotExist);
		#endregion

	}
}
