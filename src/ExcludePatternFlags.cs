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

namespace NerdyDuck.DotIgnore
{
	/// <summary>
	/// Specifies the
	/// </summary>
	[Flags]
	public enum ExcludePatternFlags
	{
		/// <summary>
		/// No flags.
		/// </summary>
		None = 0x00,

		/// <summary>
		/// Pattern does not contain any directory information, just a file name pattern.
		/// </summary>
		NoDirectory = 0x01,

		/// <summary>
		/// Pattern is a typical "ends with" pattern.
		/// </summary>
		EndsWith = 0x04,

		/// <summary>
		/// Pattern is only used for directories.
		/// </summary>
		MustBeDirectory = 0x08,

		/// <summary>
		/// Pattern is inverted (an inclusion instead of an exclusion).
		/// </summary>
		Negation = 0x10
	}
}
