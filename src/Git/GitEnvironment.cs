#region Copyright
/*******************************************************************************
 * NerdyDuck.DotIgnore - A library that implements a file filter using the
 * patterns of .gitignore.
 * 
 * The MIT License (MIT)
 *
 * Copyright (c) Daniel Kopp, dak@nerdyduck.de
 *
 * All rights reserved.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 ******************************************************************************/
#endregion

namespace NerdyDuck.DotIgnore.Git;

/// <summary>
/// Provides access to (some) Git environment variables and configuration files.
/// </summary>
public sealed class GitEnvironment
{
	private const string XDG_CONFIG_HOME = "XDG_CONFIG_HOME";

	private bool _isLoaded;
	private string _systemConfigurationPath;
	private string _globalConfigurationPath;

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

	/// <summary>
	/// Initializes a new instance of the <see cref="GitEnvironment"/> class.
	/// </summary>
	public GitEnvironment()
	{
		_isLoaded = false;
		_systemConfigurationPath = null;
		_globalConfigurationPath = null;
	}

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
}
