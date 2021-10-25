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
	/// Contains the names of documented environment variables used by Git.
	/// </summary>
	public static class GitEnvironmentVariables
	{
		/// <summary>
		/// Allows the specification of an alternate index file.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITINDEXFILEcode"/>
		public const string GIT_INDEX_FILE = "GIT_INDEX_FILE";

		/// <summary>
		/// Allows the specification of an index version for new repositories. 
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITINDEXVERSIONcode"/>
		public const string GIT_INDEX_VERSION = "GIT_INDEX_VERSION ";

		/// <summary>
		/// If the object storage directory is specified via this environment variable then the sha1 directories are created underneath it.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITOBJECTDIRECTORYcode"/>
		public const string GIT_OBJECT_DIRECTORY = "GIT_OBJECT_DIRECTORY";

		/// <summary>
		/// Specifies a list of Git object directories which can be used to search for Git objects.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITALTERNATEOBJECTDIRECTORIEScode"/>
		public const string GIT_ALTERNATE_OBJECT_DIRECTORIES = "GIT_ALTERNATE_OBJECT_DIRECTORIES";

		/// <summary>
		/// Specifies a path to use instead of the default .git for the base of the repository.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITDIRcode"/>
		public const string GIT_DIR = "GIT_DIR";

		/// <summary>
		/// Set the path to the root of the working tree.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITWORKTREEcode"/>
		public const string GIT_WORK_TREE = "GIT_WORK_TREE";

		/// <summary>
		/// Set the Git namespace.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITNAMESPACEcode"/>
		public const string GIT_NAMESPACE = "GIT_NAMESPACE";

		/// <summary>
		/// A list of directories that Git should not chdir up into while looking for a repository directory 
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITCEILINGDIRECTORIEScode"/>
		public const string GIT_CEILING_DIRECTORIES = "GIT_CEILING_DIRECTORIES";

		/// <summary>
		/// This environment variable can be set to true to tell Git not to stop at filesystem boundaries when looking for the top of a working tree.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITDISCOVERYACROSSFILESYSTEMcode"/>
		public const string GIT_DISCOVERY_ACROSS_FILESYSTEM = "GIT_DISCOVERY_ACROSS_FILESYSTEM";

		/// <summary>
		/// Non-worktree files that are normally in $GIT_DIR will be taken from this path instead.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITCOMMONDIRcode"/>
		public const string GIT_COMMON_DIR = "GIT_COMMON_DIR";

		/// <summary>
		/// The author name to use for commits.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITAUTHORNAMEcode"/>
		public const string GIT_AUTHOR_NAME = "GIT_AUTHOR_NAME";

		/// <summary>
		/// The author's email to use for commits.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITAUTHOREMAILcode"/>
		public const string GIT_AUTHOR_EMAIL = "GIT_AUTHOR_EMAIL";

		/// <summary>
		/// The authoring date to use for commits.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITAUTHORDATEcode"/>
		public const string GIT_AUTHOR_DATE = "GIT_AUTHOR_DATE";

		/// <summary>
		/// The committer name to use for commits.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITCOMMITTERNAMEcode"/>
		public const string GIT_COMMITTER_NAME = "GIT_COMMITTER_NAME";

		/// <summary>
		/// The committer's email to use for commits.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITCOMMITTEREMAILcode"/>
		public const string GIT_COMMITTER_EMAIL = "GIT_COMMITTER_EMAIL";

		/// <summary>
		/// The commit date.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITCOMMITTERDATEcode"/>
		public const string GIT_COMMITTER_DATE = "GIT_COMMITTER_DATE";

		/// <summary>
		/// set the number of context lines shown when a unified diff is created.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITDIFFOPTScode"/>
		public const string GIT_DIFF_OPTS = "GIT_DIFF_OPTS";

		/// <summary>
		/// The program to call instead of the default diff invocation.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITEXTERNALDIFFcode"/>
		public const string GIT_EXTERNAL_DIFF = "GIT_EXTERNAL_DIFF";

		/// <summary>
		/// A 1-based counter incremented by one for every path.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITDIFFPATHCOUNTERcode"/>
		public const string GIT_DIFF_PATH_COUNTER = "GIT_DIFF_PATH_COUNTER";

		/// <summary>
		/// The total number of paths.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITDIFFPATHTOTALcode"/>
		public const string GIT_DIFF_PATH_TOTAL = "GIT_DIFF_PATH_TOTAL";

		/// <summary>
		/// A number controlling the amount of output shown by the recursive merge strategy.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITMERGEVERBOSITYcode"/>
		public const string GIT_MERGE_VERBOSITY = "GIT_MERGE_VERBOSITY";

		/// <summary>
		/// The pager method to use for output. If it is set to an empty string or to the value "cat", Git will not launch a pager. 
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITPAGERcode"/>
		public const string GIT_PAGER = "GIT_PAGER";

		/// <summary>
		///  It is used by several Git commands when, on interactive mode, an editor is to be launched.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITEDITORcode"/>
		public const string GIT_EDITOR = "GIT_EDITOR";

		/// <summary>
		/// <c>git fetch</c> and <c>git push</c> will use the specified command instead of ssh when they need to connect to a remote system.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITSSHcode"/>
		public const string GIT_SSH = "GIT_SSH";

		/// <summary>
		/// <c>git fetch</c> and <c>git push</c> will use the specified command instead of ssh when they need to connect to a remote system.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITSSHCOMMANDcode"/>
		public const string GIT_SSH_COMMAND = "GIT_SSH_COMMAND";

		/// <summary>
		/// Overrides Git’s autodetection whether <c>GIT_SSH</c>/<c>GIT_SSH_COMMAND</c>/<c>core.sshCommand</c> refer to OpenSSH, plink or tortoiseplink.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITSSHVARIANTcode"/>
		public const string GIT_SSH_VARIANT = "GIT_SSH_VARIANT";

		/// <summary>
		/// Git commands which need to acquire passwords or passphrases (e.g. for HTTP or IMAP authentication) will call this program.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITASKPASScode"/>
		public const string GIT_ASKPASS = "GIT_ASKPASS";

		/// <summary>
		/// If this environment variable is set to 0, git will not prompt on the terminal.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITTERMINALPROMPTcode"/>
		public const string GIT_TERMINAL_PROMPT = "GIT_TERMINAL_PROMPT";

		/// <summary>
		/// Whether to skip reading settings from the system-wide gitconfig file.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITCONFIGNOSYSTEMcode"/>
		public const string GIT_CONFIG_NOSYSTEM = "GIT_CONFIG_NOSYSTEM";

		/// <summary>
		/// Commands such as <c>git blame</c> (in incremental mode), <c>git rev-list</c>, <c>git log</c>, <c>git check-attr</c> and <c>git check-ignore</c> will force a flush of the output stream after each record has been flushed. 
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITFLUSHcode"/>
		public const string GIT_FLUSH = "GIT_FLUSH";

		/// <summary>
		/// Enables general trace messages, e.g. alias expansion, built-in command execution and external command execution.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITTRACEcode"/>
		public const string GIT_TRACE = "GIT_TRACE";

		/// <summary>
		/// Enables trace messages for the filesystem monitor extension.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITTRACEFSMONITORcode"/>
		public const string GIT_TRACE_FSMONITOR = "GIT_TRACE_FSMONITOR";

		/// <summary>
		/// Enables trace messages for all accesses to any packs.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITTRACEPACKACCESScode"/>
		public const string GIT_TRACE_PACK_ACCESS = "GIT_TRACE_PACK_ACCESS";

		/// <summary>
		/// Enables trace messages for all packets coming in or out of a given program.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITTRACEPACKETcode"/>
		public const string GIT_TRACE_PACKET = "GIT_TRACE_PACKET";

		/// <summary>
		/// Enables tracing of packfiles sent or received by a given program.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITTRACEPACKFILEcode"/>
		public const string GIT_TRACE_PACKFILE = "GIT_TRACE_PACKFILE";

		/// <summary>
		/// Enables performance related trace messages, e.g. total execution time of each Git command.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITTRACEPERFORMANCEcode"/>
		public const string GIT_TRACE_PERFORMANCE = "GIT_TRACE_PERFORMANCE";

		/// <summary>
		/// Enables trace messages printing the .git, working tree and current working directory after Git has completed its setup phase.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITTRACESETUPcode"/>
		public const string GIT_TRACE_SETUP = "GIT_TRACE_SETUP";

		/// <summary>
		/// Enables trace messages that can help debugging fetching / cloning of shallow repositories.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITTRACESHALLOWcode"/>
		public const string GIT_TRACE_SHALLOW = "GIT_TRACE_SHALLOW";

		/// <summary>
		/// Enables a curl full trace dump of all incoming and outgoing data, including descriptive information, of the git transport protocol.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITTRACECURLcode"/>
		public const string GIT_TRACE_CURL = "GIT_TRACE_CURL";

		/// <summary>
		/// When a curl trace is enabled, do not dump data.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITTRACECURLNODATAcode"/>
		public const string GIT_TRACE_CURL_NO_DATA = "GIT_TRACE_CURL_NO_DATA";

		/// <summary>
		/// When a curl trace is enabled, whenever a "Cookies:" header sent by the client is dumped, values of cookies whose key is in that list are redacted.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITREDACTCOOKIEScode"/>
		public const string GIT_REDACT_COOKIES = "GIT_REDACT_COOKIES";

		/// <summary>
		/// Treat all pathspecs literally, rather than as glob patterns.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITLITERALPATHSPECScode"/>
		public const string GIT_LITERAL_PATHSPECS = "GIT_LITERAL_PATHSPECS";

		/// <summary>
		/// Treat all pathspecs as glob patterns (aka "glob" magic).
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITGLOBPATHSPECScode"/>
		public const string GIT_GLOB_PATHSPECS = "GIT_GLOB_PATHSPECS";

		/// <summary>
		/// Treat all pathspecs as literal (aka "literal" magic).
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITNOGLOBPATHSPECScode"/>
		public const string GIT_NOGLOB_PATHSPECS = "GIT_NOGLOB_PATHSPECS";

		/// <summary>
		/// Treat all pathspecs as case-insensitive.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITICASEPATHSPECScode"/>
		public const string GIT_ICASE_PATHSPECS = "GIT_ICASE_PATHSPECS";

		/// <summary>
		/// The command name to store in log when a ref is updated.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITREFLOGACTIONcode"/>
		public const string GIT_REFLOG_ACTION = "GIT_REFLOG_ACTION";

		/// <summary>
		/// Include broken or badly named refs when iterating over lists of refs.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITREFPARANOIAcode"/>
		public const string GIT_REF_PARANOIA = "GIT_REF_PARANOIA";

		/// <summary>
		/// Behave as if <c>protocol.allow</c> is set to never, and each of the listed protocols has <c>protocol.&lt;name&gt;.allow</c> set to always (overriding any existing configuration).
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITALLOWPROTOCOLcode"/>
		public const string GIT_ALLOW_PROTOCOL = "GIT_ALLOW_PROTOCOL";

		/// <summary>
		/// Prevent protocols used by fetch/push/clone which are configured to the <c>user</c> state. 
		/// </summary>
		public const string GIT_PROTOCOL_FROM_USER = "GIT_PROTOCOL_FROM_USER";

		/// <summary>
		/// For internal use only. Used in handshaking the wire protocol.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITPROTOCOLcode"/>
		public const string GIT_PROTOCOL = "GIT_PROTOCOL";

		/// <summary>
		/// Git will complete any requested operation without performing any optional sub-operations that require taking a lock.
		/// </summary>
		/// <see cref="https://git-scm.com/docs/git#git-codeGITOPTIONALLOCKScode"/>
		public const string GIT_OPTIONAL_LOCKS = "GIT_OPTIONAL_LOCKS";

		/// <summary>
		/// Windows-only: allow redirecting the standard input handle a path specified by the environment variable.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITREDIRECTSTDINcode"/>
		public const string GIT_REDIRECT_STDIN = "GIT_REDIRECT_STDIN";

		/// <summary>
		/// Windows-only: allow redirecting the standard output handle a path specified by the environment variable.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITREDIRECTSTDOUTcode"/>
		public const string GIT_REDIRECT_STDOUT = "GIT_REDIRECT_STDOUT";

		/// <summary>
		/// Windows-only: allow redirecting the standard error handle a path specified by the environment variable.
		/// </summary>
		/// <seealso cref="https://git-scm.com/docs/git#git-codeGITREDIRECTSTDERRcode"/>
		public const string GIT_REDIRECT_STDERR = "GIT_REDIRECT_STDERR";
	}
}
