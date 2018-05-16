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

namespace NerdyDuck.DotIgnore.FileSystem
{
	/// <summary>
	/// Represents a file or directory managed by its <see cref="IFileSystemProvider"/>.
	/// </summary>
	public interface IFileSystemObject
	{
		#region Properties
		/// <summary>
		/// When implemented, gets the date and time when the object was created.
		/// </summary>
		/// <value>The creation date and time of the current <see cref="IFileSystemObject"/>.</value>
		/// <exception cref="InvalidOperationException">The file or directory does not exist.</exception>
		/// <exception cref="IOException">File information cannot be accessed.</exception>
		DateTimeOffset Created
		{
			get;
		}

		/// <summary>
		/// When implemented, gets a value indicating whether the file or directory represented by the current <see cref="IFileSystemObject"/> exists.
		/// </summary>
		/// <value><see langword="true"/>, if the object exists; otherwise, <see langword="false"/>.</value>
		bool Exists
		{
			get;
		}

		/// <summary>
		/// When implemented, gets a value indicating whether the current <see cref="IFileSystemObject"/> is a file or a directory.
		/// </summary>
		/// <value><see langword="true"/>, if the object is a directory; otherwise, <see langword="false"/>.</value>
		bool IsDirectory
		{
			get;
		}

		/// <summary>
		/// When implemented, gets the date and time when the object was last modified.
		/// </summary>
		/// <value>The modification date and time of the current <see cref="IFileSystemObject"/>.</value>
		/// <exception cref="InvalidOperationException">The file or directory does not exist.</exception>
		/// <exception cref="IOException">File information cannot be accessed.</exception>
		DateTimeOffset LastModified
		{
			get;
		}

		/// <summary>
		/// When implemented, gets the length of the current <see cref="IFileSystemObject"/>, if it is a file.
		/// </summary>
		/// <value>If <see cref="IFileSystemObject"/> is a file, the length of the file; otherwise, 0.</value>
		/// <exception cref="InvalidOperationException">The file does not exist.</exception>
		/// <exception cref="IOException">File information cannot be accessed.</exception>
		long Length
		{
			get;
		}

		/// <summary>
		/// When implemented, gets the name of the file or directory, not including any path.
		/// </summary>
		/// <value>The name of the <see cref="IFileSystemObject"/>.</value>
		string Name
		{
			get;
		}

		/// <summary>
		/// When implemented, gets the path to the file or directory (including the file name).
		/// </summary>
		/// <value>The full path to the file or directory, or <see langword="null"/>, if the object is not directly accessible.</value>
		string PhysicalPath
		{
			get;
		}

		/// <summary>
		/// When implemented, gets the path to the file or directory, relative to the <see cref="IFileSystemProvider.Root"/>.
		/// </summary>
		/// <value>The relative path to the file or directory.</value>
		string RelativePath
		{
			get;
		}
		#endregion
	}
}
