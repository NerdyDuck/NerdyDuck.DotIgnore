# Development Notes

## Exact reengineering of git mechanics
### Ignore sources
#### .gitignore
- in same folder as filtered file
- in parent folders
  - How to combine? Priority?
  - Stop at worktree root folder, or also include files higher in folder hierarchy, but outside of repo?
#### &lt;repository&gt;/.git/info/exclude
How to combine? Priority?
#### path in core.excludesFile
How to combine? Priority?

### Config sources
#### %PROGRAMDATA%\Git\config (Windows)
--system
#### $(prefix)/etc/gitconfig (Windows)
- --system
- Where?
#### $XDG_CONFIG_HOME/git/config or $HOME/.config/git/config (Linux)
- -- global
- On Windows: %USERPROFILE%\\.config\git\gitk (not same?)
#### ~/.gitconfig (Linux/Windows)
- --global
- On Windows: %USERPROFILE%\\.gitconfig
#### $GIT_DIR/config

### Environment variables (also CLI params)
#### GIT_CONFIG
Use instead of .git\config
#### GIT_CONFIG_NOSYSTEM
Do not use system config file, only user and repo
#### GIT_DIR
Use instead of default .git folder in worktree
#### GIT_WORK_TREE
- Path to worktree root
- Doesn't make much sense in context of library?
#### GIT_CEILING_DIRECTORIES
Directories not to look at when looking for repo
#### GIT_DISCOVERY_ACROSS_FILESYSTEM
Do not stop at filesystem boundaries when looking for worktree top
#### GIT_COMMON_DIR
Files usually in GIT_DIR are taken from this folder.
#### GIT_CONFIG_NOSYSTEM
Do not use system config file
#### GIT_LITERAL_PATHSPECS
Treat pathspecs literally, not as glob patterns.
#### GIT_GLOB_PATHSPECS
Treat all pathspecs as glob patterns.
#### GIT_NOGLOB_PATHSPECS
Treat all pathspecs as literal
#### GIT_ICASE_PATHSPECS
Treat all pathspecs as case-insensitive.
