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
using System.Globalization;
using System.IO;
using System.Text;

namespace NerdyDuck.DotIgnore.FileSystem.Physical
{
	/// <summary>
	/// Represents path on a physical file system (local or UNC paths).
	/// </summary>
	public class PhysicalFileSystemProvider : IFileSystemProvider
	{
		#region Private fields
		private static readonly char[] _pathSeparators = new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };
		private readonly string _root;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the base path of the underlying file system that the <see cref="IFileSystemProvider"/> manages.
		/// </summary>
		/// <value>The rooted path of the drive or directory that the <see cref="PhysicalFileSystemProvider"/> represents.</value>
		public string Root => _root;
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="PhysicalFileSystemProvider"/> class with the specified root path.
		/// </summary>
		/// <param name="root">The base path of the underlying file system that the <see cref="IFileSystemProvider"/> manages.</param>
		/// <exception cref="ArgumentNullException"><paramref name="root"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="root"/> is empty, not a proper local or UNC path, or a relative path.</exception>
		/// <exception cref="DirectoryNotFoundException">The path in <paramref name="root"/> does not exist or could not be accessed.</exception>
		public PhysicalFileSystemProvider(string root)
		{
			if (root == null)
			{
				throw new ArgumentNullException(nameof(root));
			}

			if (!Path.IsPathRooted(root))
			{
				throw new ArgumentException(TextResources.PhysicalFileSystemProvider_ctor_PathNotAbsolute, nameof(root));
			}

			_root = Utils.EnsureTrailingSlash(Path.GetFullPath(root));

			if (!Directory.Exists(_root))
			{
				throw new DirectoryNotFoundException(string.Format(CultureInfo.CurrentCulture, TextResources.PhysicalFileSystemProvider_ctor_PathNotFound, _root));
			}
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Gets the contents of the directory at the specified relative path.
		/// </summary>
		/// <param name="relativePath">The path to the directory, relative to the base path of the <see cref="IFileSystemProvider"/>.</param>
		/// <returns>An <see cref="IDirectoryContentCollection"/> instance.</returns>
		public IDirectoryContentCollection GetDirectoryContents(string relativePath)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(relativePath) || Utils.HasInvalidPathChars(relativePath) || Utils.IsUncPath(relativePath))
				{
					return Empty.NotFoundDirectoryContentCollection.Instance;
				}

				relativePath = relativePath.TrimStart(_pathSeparators);

				if (Path.IsPathRooted(relativePath))
				{
					return Empty.NotFoundDirectoryContentCollection.Instance;
				}

				string fullPath = GetFullPath(relativePath);
				if (fullPath == null)
				{
					return Empty.NotFoundDirectoryContentCollection.Instance;
				}

				return new PhysicalDirectoryContentCollection(fullPath, _root.Length);
			}
			catch (DirectoryNotFoundException)
			{
			}
			catch (IOException)
			{
			}

			return Empty.NotFoundDirectoryContentCollection.Instance;
		}

		/// <summary>
		/// Gets the <see cref="IFileSystemObject"/> representing the file or directory at the specified relative path.
		/// </summary>
		/// <param name="relativePath">The path to the file or directory, relative to the base path of the <see cref="IFileSystemProvider"/>.</param>
		/// <returns>An <see cref="IFileSystemObject"/> instance.</returns>
		public IFileSystemObject GetFileSystemObject(string relativePath)
		{
			if (string.IsNullOrWhiteSpace(relativePath) || Utils.HasInvalidPathChars(relativePath) || Utils.IsUncPath(relativePath))
			{
				return new Empty.NotFoundFileObject(relativePath);
			}


			relativePath = relativePath.TrimStart(_pathSeparators);

			if (Path.IsPathRooted(relativePath))
			{
				return new Empty.NotFoundFileObject(relativePath);
			}

			string fullPath = GetFullPath(relativePath);
			if (fullPath == null)
			{
				return new Empty.NotFoundFileObject(relativePath);
			}

			FileInfo fileInfo = new FileInfo(fullPath);

			return new PhysicalFile(fileInfo, relativePath);
		}
		#endregion

		#region Private methods
		private string GetFullPath(string path)
		{
			if (Utils.PathNavigatesAboveRoot(path))
			{
				return null;
			}

			string fullPath;
			try
			{
				fullPath = Path.GetFullPath(Path.Combine(Root, path));
			}
			catch
			{
				return null;
			}

			if (!IsUnderneathRoot(fullPath))
			{
				return null;
			}

			return fullPath;
		}

		private bool IsUnderneathRoot(string fullPath) => fullPath.StartsWith(_root, StringComparison.OrdinalIgnoreCase);
		#endregion
	}
}
