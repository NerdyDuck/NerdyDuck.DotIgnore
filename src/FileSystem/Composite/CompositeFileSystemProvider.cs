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
using System.Text;

namespace NerdyDuck.DotIgnore.FileSystem.Composite
{
	/// <summary>
	/// Looks up files using a collection of <see cref="IFileSystemProvider"/>s.
	/// </summary>
	public class CompositeFileSystemProvider : IFileSystemProvider
	{
		#region Private fields
		private readonly IEnumerable<IFileSystemProvider> _providers;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the base path of the underlying file system that the <see cref="IFileSystemProvider"/> manages.
		/// </summary>
		/// <value>Always returns <see langword="null"/>.</value>
		public string Root => null;
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="CompositeFileSystemProvider"/> class with a list of file system providers.
		/// </summary>
		/// <param name="providers">A list of <see cref="IFileSystemProvider"/>s to request files from.</param>
		public CompositeFileSystemProvider(IEnumerable<IFileSystemProvider> providers)
		{
			_providers = providers ?? throw new ArgumentNullException(nameof(providers));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CompositeFileSystemProvider"/> class with one or more file system providers.
		/// </summary>
		/// <param name="providers">A list of <see cref="IFileSystemProvider"/>s to request files from.</param>
		public CompositeFileSystemProvider(params IFileSystemProvider[] providers)
		{
			_providers = providers ?? throw new ArgumentNullException(nameof(providers));
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Queries all file system providers and returns the aggregated contents of all directories matching the specified relative path.
		/// </summary>
		/// <param name="relativePath">The path to the directory, relative to the base path of each <see cref="IFileSystemProvider"/>.</param>
		/// <returns>An <see cref="IDirectoryContentCollection"/> instance.</returns>
		public IDirectoryContentCollection GetDirectoryContents(string relativePath) => new CompositeDirectoryContentCollection(_providers, relativePath);

		/// <summary>
		/// Gets the <see cref="IFileSystemObject"/> representing the file or directory at the specified relative path.
		/// </summary>
		/// <param name="relativePath">The path to the file or directory, relative to the base path of the <see cref="IFileSystemProvider"/>s.</param>
		/// <returns>An <see cref="IFileSystemObject"/> instance.</returns>
		/// <remarks>The file system providers are queried for the relative path. The value of the first provider that returns a match is returned.</remarks>
		public IFileSystemObject GetFileSystemObject(string relativePath)
		{
			foreach (IFileSystemProvider provider in _providers)
			{
				IFileSystemObject obj = provider.GetFileSystemObject(relativePath);
				if (obj != null && obj.Exists)
				{
					return obj;
				}
			}

			return new Empty.NotFoundFileObject(relativePath);
		}
		#endregion
	}
}
