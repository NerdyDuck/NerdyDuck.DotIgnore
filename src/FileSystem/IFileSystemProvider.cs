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

namespace NerdyDuck.DotIgnore.FileSystem
{
	/// <summary>
	/// Represents an object that manages (part of) a file system.
	/// </summary>
	public interface IFileSystemProvider
	{
		#region Properties
		/// <summary>
		/// When implemented, gets the base path of the underlying file system that the <see cref="IFileSystemProvider"/> manages.
		/// </summary>
		/// <value>A path, specific to the type of <see cref="IFileSystemProvider"/>. May be <see langword="null"/> if the <see cref="IFileSystemProvider"/> does not have a concept of a base path.</value>
		string Root
		{
			get;
		}
		#endregion

		#region Methods
		/// <summary>
		/// When implemented, gets the contents of the directory at the specified relative path.
		/// </summary>
		/// <param name="relativePath">The path to the directory, relative to the base path of the <see cref="IFileSystemProvider"/>.</param>
		/// <returns>An <see cref="IDirectoryContentCollection"/> instance.</returns>
		IDirectoryContentCollection GetDirectoryContents(string relativePath);

		/// <summary>
		/// When implemented, gets the <see cref="IFileSystemObject"/> representing the file or directory at the specified relative path.
		/// </summary>
		/// <param name="relativePath">The path to the file or directory, relative to the base path of the <see cref="IFileSystemProvider"/>.</param>
		/// <returns>An <see cref="IFileSystemObject"/> instance.</returns>
		IFileSystemObject GetFileSystemObject(string relativePath);
		#endregion
	}
}
