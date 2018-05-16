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

namespace NerdyDuck.DotIgnore.FileSystem.Empty
{
	/// <summary>
	/// Represents a file or directory that does not exist.
	/// </summary>
	public class NotFoundFileObject : IFileSystemObject
	{
		#region Private fields
		private readonly string _relativePath;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the date and time when the object was created.
		/// </summary>
		/// <value>Always throws a <see cref="InvalidOperationException"/>.</value>
		/// <exception cref="InvalidOperationException">The file or directory does not exist.</exception>
		public DateTimeOffset Created => throw FileDoesNotExistException();

		/// <summary>
		/// Gets a value indicating whether the file or directory represented by the current <see cref="IFileSystemObject"/> exists.
		/// </summary>
		/// <value>Always returns <see langword="false"/>.</value>
		public bool Exists => false;

		/// <summary>
		/// Gets a value indicating whether the current <see cref="IFileSystemObject"/> is a file or a directory.
		/// </summary>
		/// <value>Always returns <see langword="false"/>.</value>
		public bool IsDirectory => false;

		/// <summary>
		/// Gets the date and time when the object was last modified.
		/// </summary>
		/// <value>Always throws a <see cref="InvalidOperationException"/>.</value>
		/// <exception cref="InvalidOperationException">The file or directory does not exist.</exception>
		public DateTimeOffset LastModified => throw FileDoesNotExistException();

		/// <summary>
		/// Gets the length of the current <see cref="IFileSystemObject"/>, if it is a file.
		/// </summary>
		/// <value>Always throws a <see cref="InvalidOperationException"/>.</value>
		/// <exception cref="InvalidOperationException">The file or directory does not exist.</exception>
		public long Length => throw FileDoesNotExistException();

		/// <summary>
		/// Gets the name of the file or directory, not including any path.
		/// </summary>
		/// <value>Always returns <see cref="string.Empty"/>.</value>
		public string Name => string.Empty;

		/// <summary>
		/// Gets the path to the file or directory (including the file name).
		/// </summary>
		/// <value>Always returns<see langword="null"/>.</value>
		public string PhysicalPath => null;

		/// <summary>
		/// Gets the path to the file or directory, relative to the <see cref="IFileSystemProvider.Root"/>.
		/// </summary>
		/// <value>The relative path to the file or directory.</value>
		public string RelativePath => _relativePath;
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="NotFoundFileObject"/> class with the relative path of the object.
		/// </summary>
		/// <param name="relativePath">The relative path to the file or directory.</param>
		public NotFoundFileObject(string relativePath)
		{
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
