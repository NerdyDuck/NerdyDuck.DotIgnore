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

namespace NerdyDuck.DotIgnore.FileSystem
{
	/// <summary>
	/// Represents the contents of a directory managed by a <see cref="IFileSystemProvider"/>.
	/// </summary>
	public interface IDirectoryContentCollection : IEnumerable<IFileSystemObject>
	{
		#region Properties
		/// <summary>
		/// When implemented, gets a value indicating whether the directory exists.
		/// </summary>
		/// <value><see langword="true"/>, if the directory exists; otherwise, <see langword="false"/>.</value>
		bool Exists
		{
			get;
		}
		#endregion
	}
}
