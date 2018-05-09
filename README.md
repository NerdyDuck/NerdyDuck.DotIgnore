# ![Logo](media/NerdyDuck.DotIgnore-128x128.png) NerdyDuck.DotIgnore
A library (.NET Standard 2.0) that implements a file filter using the patterns of .gitignore.

### Status
Under development. Not functional.

### Description
[Git](https://git-scm.com/) version control uses a file (`.gitignore`) to control which files in the repository folder are actually managed by the version control.
The file contains a list of patterns for files or folders to exclude (or include).
[Microsoft Team Foundation Server](https://www.visualstudio.com/tfs/) and [Microsoft Visual Studio Team Services](https://www.visualstudio.com/team-services/) use the same format for their repositories (`.gitignore` for Git, `.tfignore` for [TFVC](https://docs.microsoft.com/en-us/vsts/tfvc/overview)).
The specification for the file format can be found at [https://git-scm.com/docs/gitignore](https://git-scm.com/docs/gitignore).

Tools working with such repositories may want to traverse the files and folders of the repository directory using the same filter as the used source control provider.
Other projects that create a kind of document management system may want to implement the same filter capabilities as Git.
This library provides the functionality to integrate this filter mechanic into your own projects.
It is built on interfaces and abstract classes, so you can implement the filter for other document systems than the local file system, or have the filter definitions stored in a way other than a file.

### Platform
This library is compiled for .NET Standard 2.0 (`netstandard2.0`), so it is compatible with projects compiled for
- .NET Core 2.0 or later
- .NET Framework 4.6.1 or later
- Mono 5.4 or later
- Xamarin.iOS 10.14 or later
- Xamarin.Mac 3.8 or later
- Xamarin.Android 8.0 or later
- Universal Windows Platform 10.0.16299 or later

### Languages
The neutral resource language for all texts is English (en-US). Currently, the only localization available is German (de-DE). If you like to add other languages, feel free to send a pull request with the translated resources!

### How to get (TODO)
- Use the NuGet package from my [MyGet](https://www.myget.org) feed: [https://www.myget.org/F/nerdyduck-release/api/v3/index.json](https://www.myget.org/F/nerdyduck-release/api/v3/index.json). If you need to debug the library, get the debug symbols from the same feed: [https://www.myget.org/F/nerdyduck-release/symbols/](https://www.myget.org/F/nerdyduck-release/symbols/).
- Download the binaries from the [Releases](../../releases/) page.
- You can clone the repository and compile the libraries yourself (see the [Wiki](../../wiki/) for requirements).

### More information (TODO)
For examples and a complete class reference, please see the [Wiki](../../wiki/).

### License
The project is licensed under the [Apache License, Version 2.0](LICENSE).

### History
- 1.0.0 / TBD

### Attributions
- Initial idea for project based on [MAB.DotIgnore](https://github.com/markashleybell/MAB.DotIgnore) by @markashleybell
- [Project logo](media/NerdyDuck.DotIgnore.svg) based on original [Git logo](https://git-scm.com/downloads/logos) licensed by [Jason Long](https://twitter.com/jasonlong) under the [Creative Commons Attribution 3.0 Unported License](https://creativecommons.org/licenses/by/3.0/).
