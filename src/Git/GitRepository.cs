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

namespace NerdyDuck.DotIgnore.Git
{
	/// <summary>
	/// Provides access to (some) Git repository settings.
	/// </summary>
	public sealed class GitRepository
	{
		#region Private fields
		private bool _isLoaded;
		private bool _isEnvironmentExternal;
		private string _repositoryPath;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the current Git environment settings.
		/// </summary>
		public GitEnvironment Environment
		{
			get; private set;
		}

		/// <summary>
		/// Gets the path to the Git repository root.
		/// </summary>
		/// <remarks>The absolute path to the root folder of the Git repository. May not be <see langword="null"/> or empty.</remarks>
		public string RepositoryPath
		{
			get
			{
				AssertLoaded();
				return _repositoryPath;
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="GitRepository"/> class with the specified Git repository path.
		/// </summary>
		/// <param name="path">The path to the Git repository root.</param>
		public GitRepository(string path)
			: this(path, new GitEnvironment())
		{
			_isEnvironmentExternal = false;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GitRepository"/> class with the specified Git repository path and environment.
		/// </summary>
		/// <param name="path">The path to the Git repository root.</param>
		/// <param name="environment">The current Git environment settings.</param>
		public GitRepository(string path, GitEnvironment environment)
		{
			_isLoaded = false;
			_isEnvironmentExternal = true;
			Environment = environment ?? throw new ArgumentNullException(nameof(environment));
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentException(TextResources.GitRepository_ctor_PathEmpty, nameof(path));
			}
			_repositoryPath = path;

		}
		#endregion

		#region Public methods
		#region Load
		/// <summary>
		/// Loads or reloads the settings of the repository.
		/// </summary>
		/// <returns>The current instance.</returns>
		public GitRepository Load()
		{
			if (!_isEnvironmentExternal)
			{
				Environment.Load();
			}

			// TODO
			_isLoaded = true;
			return this;
		}
		#endregion

		#region GetSetting
		/// <summary>
		/// Gets the value of the setting with the specified name from the specified section.
		/// </summary>
		/// <param name="section">The name of the section that contains the setting.</param>
		/// <param name="name">The name of the setting.</param>
		/// <returns>A string, if the setting was found; otherwise, <see langword="null"/>.</returns>
		public string GetSetting(string section, string name)
		{
			// TODO
			throw new NotImplementedException();
		}
		#endregion
		#endregion

		#region Private methods
		/// <summary>
		/// Loads the repository settings, if no settings are loaded yet.
		/// </summary>
		private void AssertLoaded()
		{
			if (!_isLoaded)
			{
				Load();
			}
		}
		#endregion
	}
}
