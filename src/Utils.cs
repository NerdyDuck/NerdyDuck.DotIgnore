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
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Primitives;

namespace NerdyDuck.DotIgnore
{
	/// <summary>
	/// Contains global methods.
	/// </summary>
	internal static class Utils
	{
		private static readonly char[] _invalidFileNameChars = Path.GetInvalidFileNameChars().Where(c => c != Path.DirectorySeparatorChar && c != Path.AltDirectorySeparatorChar).ToArray();

		private static readonly char[] _pathSeparators = new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };


		internal static string EnsureTrailingSlash(string path) => (!string.IsNullOrEmpty(path) && path[path.Length - 1] != Path.DirectorySeparatorChar) ? path + Path.DirectorySeparatorChar : path;

		internal static bool HasInvalidPathChars(string path) => path.IndexOfAny(_invalidFileNameChars) != -1;

		internal static bool IsUncPath(string path) => path.StartsWith(@"\\", StringComparison.Ordinal) && path.IndexOf('\\', 2) > 2;

		internal static bool PathNavigatesAboveRoot(string path)
		{
			StringTokenizer tokenizer = new StringTokenizer(path, _pathSeparators);
			int depth = 0;

			foreach (StringSegment segment in tokenizer)
			{
				if (segment.Equals(".", StringComparison.Ordinal) || segment.Equals("", StringComparison.Ordinal))
				{
					continue;
				}
				else if (segment.Equals("..", StringComparison.Ordinal))
				{
					depth--;

					if (depth == -1)
					{
						return true;
					}
				}
				else
				{
					depth++;
				}
			}

			return false;
		}
	}
}
