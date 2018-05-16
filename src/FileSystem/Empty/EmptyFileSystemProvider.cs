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

namespace NerdyDuck.DotIgnore.FileSystem.Empty
{
	/// <summary>
	/// A file system provider that contains no files or directories.
	/// </summary>
	public class EmptyFileSystemProvider : IFileSystemProvider
	{
		#region Properties
		/// <summary>
		/// Gets the base path of the underlying file system that the <see cref="IFileSystemProvider"/> manages.
		/// </summary>
		/// <value>Always returns <see langword="null"/>.</value>
		public string Root => null;
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="EmptyFileSystemProvider"/> class.
		/// </summary>
		public EmptyFileSystemProvider()
		{
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Gets the contents of the directory at the specified relative path.
		/// </summary>
		/// <param name="relativePath">The path to the directory, relative to the base path of the <see cref="IFileSystemProvider"/>.</param>
		/// <returns>Always returns an instance of <see cref="NotFoundDirectoryContentCollection"/>.</returns>
		public IDirectoryContentCollection GetDirectoryContents(string relativePath) => NotFoundDirectoryContentCollection.Instance;

		/// <summary>
		/// Gets the <see cref="IFileSystemObject"/> representing the file or directory at the specified relative path.
		/// </summary>
		/// <param name="relativePath">The path to the file or directory, relative to the base path of the <see cref="IFileSystemProvider"/>.</param>
		/// <returns>Always returns an instance of <see cref="NotFoundFileObject"/>.</returns>
		public IFileSystemObject GetFileSystemObject(string relativePath) => new NotFoundFileObject(relativePath);
		#endregion
	}
}
