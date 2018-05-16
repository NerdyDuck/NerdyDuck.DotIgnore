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

namespace NerdyDuck.DotIgnore.FileSystem.Composite
{
	/// <summary>
	/// Represents the result of a call composition of <see cref="IFileSystemProvider.GetDirectoryContents(string)"/> for a list of <see cref="IFileSystemProvider"/>s and a path.
	/// </summary>
	public class CompositeDirectoryContentCollection : IDirectoryContentCollection
	{
		#region Private fields
		private readonly IEnumerable<IFileSystemProvider> _providers;
		private readonly string _relativePath;
		private List<IFileSystemObject> _contents;
		private bool _exists;
		private List<IDirectoryContentCollection> _directories;
		#endregion

		#region Properties
		/// <summary>
		/// Gets a value indicating whether the directory exists in at least one of the <see cref="IFileSystemProvider"/>s.
		/// </summary>
		/// <value><see langword="true"/>, if the directory exists; otherwise, <see langword="false"/>.</value>
		public bool Exists
		{
			get
			{
				EnsureDirectoriesInitialized();
				return _exists;
			}
		}
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="CompositeDirectoryContentCollection"/> class with the specified list of file system providers and a relative path.
		/// </summary>
		/// <param name="providers">A list of <see cref="IFileSystemProvider"/>s to find the <paramref name="relativePath"/> in.</param>
		/// <param name="relativePath">The relative directory path to find in the <paramref name="providers"/>.</param>
		public CompositeDirectoryContentCollection(IEnumerable<IFileSystemProvider> providers, string relativePath)
		{
			_providers = providers ?? throw new ArgumentNullException(nameof(providers));
			_relativePath = relativePath;
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Returns an enumerator that iterates through the <see cref="IFileSystemObject"/>s in the <see cref="IDirectoryContentCollection"/>.
		/// </summary>
		/// <returns>An instance of <see cref="IEnumerator{T}"/>.</returns>
		public IEnumerator<IFileSystemObject> GetEnumerator()
		{
			EnsureFilesInitialized();
			return _contents.GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator that iterates through the <see cref="IFileSystemObject"/>s in the <see cref="IDirectoryContentCollection"/>.
		/// </summary>
		/// <returns>An instance of <see cref="IEnumerator"/>.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			EnsureFilesInitialized();
			return _contents.GetEnumerator();
		}
		#endregion

		#region PrivateMethods
		private void EnsureFilesInitialized()
		{
			EnsureDirectoriesInitialized();
			if (_contents == null)
			{
				_contents = new List<IFileSystemObject>();
				HashSet<string> nameSet = new HashSet<string>();
				foreach (IDirectoryContentCollection directory in _directories)
				{
					foreach (IFileSystemObject obj in directory)
					{
						if (nameSet.Add(obj.Name))
						{
							_contents.Add(obj);
						}
					}
				}
			}
		}

		private void EnsureDirectoriesInitialized()
		{
			if (_directories == null)
			{
				_directories = new List<IDirectoryContentCollection>();
				foreach (IFileSystemProvider provider in _providers)
				{
					IDirectoryContentCollection contents = provider.GetDirectoryContents(_relativePath);
					if (contents != null && contents.Exists)
					{
						_exists = true;
						_directories.Add(contents);
					}
				}
			}
		}
		#endregion
	}
}
