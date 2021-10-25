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
	/// Provides access to (some) Git environment variables and configuration files.
	/// </summary>
	public sealed class GitEnvironment
	{
		#region Constants
		private const string XDG_CONFIG_HOME = "XDG_CONFIG_HOME";
		#endregion

		#region Private fields
		private bool _isLoaded;
		private string _systemConfigurationPath;
		private string _globalConfigurationPath;
		#endregion

		#region Properties
		/// <summary>
		/// Gets the path to the system configuration file.
		/// </summary>
		/// <value>The absolute path to the system configuration file. If the value is <see langword="null"/>, the file does not exist.</value>
		/// <remarks>This configuration is applied to all users on the system. The property takes any environment variables into account that may change the file.</remarks>
		public string SystemConfigurationPath
		{
			get
			{
				AssertLoaded();
				return _systemConfigurationPath;
			}
		}

		/// <summary>
		/// Gets the path to the global configuration file.
		/// </summary>
		/// <value>The absolute path to the global configuration file. If the value is <see langword="null"/>, the file does not exist.</value>
		/// <remarks>This configuration is specific to each user. The property takes any environment variables into account that may change the file.</remarks>
		public string GlobalConfigurationPath
		{
			get
			{
				AssertLoaded();
				return _globalConfigurationPath;
			}
		}
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="GitEnvironment"/> class.
		/// </summary>
		public GitEnvironment()
		{
			_isLoaded = false;
			_systemConfigurationPath = null;
			_globalConfigurationPath = null;
		}
		#endregion

		#region Public methods
		#region Load
		/// <summary>
		/// Loads or reloads the settings from the environment.
		/// </summary>
		/// <returns>The current instance.</returns>
		public GitEnvironment Load()
		{
			// TODO
			_isLoaded = true;
			return this;
		}
		#endregion

		#region GetEnvironmentVariable
		/// <summary>
		/// Gets the value of the environment variable with the specified name.
		/// </summary>
		/// <param name="name">The name of the environment variable.</param>
		/// <returns>A string, if the variable was found; otherwise, <see langword="null"/>.</returns>
		public string GetEnvironmentVariable(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException(TextResources.GitEnvironment_GetEnvironmentVariables_NameEmpty, nameof(name));
			}

			return Environment.GetEnvironmentVariable(name);
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
		/// Load the environment settings, if no settings are loaded yet.
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
