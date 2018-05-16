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
using System.Linq;

namespace NerdyDuck.DotIgnore.FileSystem.Empty
{
	/// <summary>
	/// Represents a directory and its contents that does not exist.
	/// </summary>
	public class NotFoundDirectoryContentCollection : IDirectoryContentCollection
	{
		#region Public fields
		/// <summary>
		/// The singleton instance of <see cref="NotFoundDirectoryContentCollection"/>.
		/// </summary>
		public static readonly NotFoundDirectoryContentCollection Instance = new NotFoundDirectoryContentCollection();
		#endregion

		#region Properties
		/// <summary>
		/// Gets a value indicating whether the directory exists.
		/// </summary>
		/// <value>Always returns <see langword="false"/>.</value>
		public bool Exists => false;
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="NotFoundDirectoryContentCollection"/> class.
		/// </summary>
		private NotFoundDirectoryContentCollection()
		{
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Returns an enumerator that iterates through the <see cref="IFileSystemObject"/>s in the <see cref="IDirectoryContentCollection"/>.
		/// </summary>
		/// <returns>An instance of <see cref="IEnumerator{T}"/> that contains no elements.</returns>
		public IEnumerator<IFileSystemObject> GetEnumerator() => Enumerable.Empty<IFileSystemObject>().GetEnumerator();

		/// <summary>
		/// Returns an enumerator that iterates through the <see cref="IFileSystemObject"/>s in the <see cref="IDirectoryContentCollection"/>.
		/// </summary>
		/// <returns>An instance of <see cref="IEnumerator"/> that contains no elements.</returns>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		#endregion
	}
}
