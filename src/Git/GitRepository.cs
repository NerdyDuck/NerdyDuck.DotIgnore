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
/// Provides access to (some) Git repository settings.
/// </summary>
public sealed class GitRepository
{
	private bool _isLoaded;
	private bool _isEnvironmentExternal;
	private string _repositoryPath;

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
	/// Loads the repository settings, if no settings are loaded yet.
	/// </summary>
	private void AssertLoaded()
	{
		if (!_isLoaded)
		{
			Load();
		}
	}
}
