# ![Logo](media/NerdyDuck.DotIgnore.svg) NerdyDuck.DotIgnore

A library that implements a file filter using the patterns of .gitignore.

### Description
[Git](https://git-scm.com/) version control uses a file (`.gitignore`) to control which files in the repository folder are actually managed by the version control.
The file contains a list of patterns for files or folders to exclude (or include).
[Microsoft Azure Devops Services](https://azure.microsoft.com/en-us/services/devops/) and [Microsoft Azure Devops Server](https://azure.microsoft.com/en-us/services/devops/server/) use the same format for their repositories (`.gitignore` for Git, `.tfignore` for [TFVC](https://docs.microsoft.com/en-us/azure/devops/repos/tfvc/what-is-tfvc?view=azure-devops)).
The specification for the file format can be found at [https://git-scm.com/docs/gitignore](https://git-scm.com/docs/gitignore).

Tools working with such repositories may want to traverse the files and folders of the repository directory using the same filter as the used source control provider.
Other projects that create a kind of document management system may want to implement the same filter capabilities as Git.
This library provides the functionality to integrate this filter mechanic into your own projects.
Its file access is based on [Microsoft.Extensions.FileProviders.Abstractions](https://docs.microsoft.com/de-de/dotnet/api/microsoft.extensions.fileproviders), that uses interfaces to represents files and folders, so you can implement the filter for other document systems than the local file system.
The **NerdyDuck.DotIgnore** library also offers abstract definitions of entries in a .gitignore file, so filter definitions can be stored in a way other than a file.

#### Platforms
- .NET Standard 2.0 (netstandard2.0), to support .NET Framework (4.6.1 and up), .NET Core (2.0 and up), Mono (5.4 and up), and the Xamarin and UWP platforms.
- .NET 5 (net5.0)
- .NET 6 (net6.0)

#### Dependencies
The project uses the following NuGet packages not issued by Microsoft as part of the BCL:
- [NerdyDuck.CodedExceptions](https://www.nuget.org/packages/NerdyDuck.CodedExceptions)
- [Microsoft.Extensions.FileProviders.Abstractions](https://www.nuget.org/packages/Microsoft.Extensions.FileProviders.Abstractions)

#### Languages
The neutral resource language for all texts is English (en-US). Currently, the only localization available is German (de-DE). If you like to add other languages, feel free to send a pull request with the translated resources!

#### How to get
- Use the NuGet package (include debug symbol files and supports [SourceLink](https://github.com/dotnet/sourcelink): https://www.nuget.org/packages/NerdyDuck.DotIgnore
- Download the binaries from the [Releases](../../releases/) page.

#### More information
For examples and a complete class reference, please see the [Wiki](../../wiki/). :exclamation: **Work in progress**.

#### License
The project is licensed under the [MIT License](LICENSE).

#### Attributions
- Initial idea for project based on [MAB.DotIgnore](https://github.com/markashleybell/MAB.DotIgnore) by @markashleybell
- [Project logo](media/NerdyDuck.DotIgnore.svg) based on original [Git logo](https://git-scm.com/downloads/logos) licensed by [Jason Long](https://twitter.com/jasonlong) under the [Creative Commons Attribution 3.0 Unported License](https://creativecommons.org/licenses/by/3.0/).



#### History
##### TBD / 1.0.0-rc.1 / DAK
- Initial release
