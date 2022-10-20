// See https://aka.ms/new-console-template for more information
// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Runtime.CompilerServices;
using LibGit2Sharp;
using RedirectRepositoryCloner;
using static RedirectRepositoryCloner.GhostRepository;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace RedirectRepositoryCloner
{
	public class Program
	{
		private const string defaultRepoUrl = "https://github.com/jishimwe/PlayMusic.git";
		private const string defaultDestPath = @"C:\Users\test\Documents\GhostRepo\PlayMusic";
		private const string defaultFileRelativePath = @"app/src/main/AndroidManifest.xml";
		private const string defaultFileRealPath = @"C:\Users\test\Documents\GhostRepo\PlayMusic\app\src\main\AndroidManifest.xml";

		static void Main(string[] args)
		{
			//HashSet<string> localFiles = new HashSet<string>();
			Dictionary<string, string> localFiles = new Dictionary<string, string>();

			if (args.Length <= 2)
			{
				Console.WriteLine("No arguments. Launching tests instances");
				DefaultExec();
				return;
			}

			string repoUrl = "", destPath = "", redirUrl = "", redirPath = "", tokenPath = "";

			for (int i = 0; i < args.Length; i += 2)
			{
				string s = args[i];
				switch (s)
				{
					case "-s":
						repoUrl = args[i + 1];
						break;

					case "-d":
						destPath = args[i + 1];
						break;

					case "-r":
						redirUrl = args[i + 1];
						break;

					case "-p":
						redirPath = args[i + 1];
						break;

					case "-t":
						tokenPath = args[i + 1];
						break;

					default:
						Console.WriteLine(args[i] + " is not a valid argument");
						PrintUsage();
						Console.WriteLine("Terminating process");
						break;
				}
			}

			if (repoUrl == "" || destPath == "" || redirPath == "" || redirUrl == "" || tokenPath == "")
			{
				Console.WriteLine("Invalid arguments");
				PrintUsage();
				Console.WriteLine("Terminating process");
				return;
			}

			GhostRepository ghostRepository = new GhostRepository(repoUrl, destPath);
			GhostRepository redirRepository = new GhostRepository(redirUrl, redirPath, true);

			Console.WriteLine(repoUrl + " cloned into the directory " + destPath);
			Console.WriteLine(redirUrl + " cloned into the directory " + redirPath);
		}

		private static void PrintUsage()
		{
			Console.WriteLine("Launch the program in a terminal with the following arguments:" +
				"\n -s <Real repository> : The url for the project repository" +
				"\n -d <Destination path>: The path where to put the cloned project repository" +
				"\n -r <Redir repository>: The url for the redir repository" +
				"\n -p <Redir path>      : The path where to put the cloned redir repository" +
				"\n -t <Path to token>   : The path to the file containing a git token");
			Console.WriteLine();
		}

		private static void DefaultExec()
		{

			Console.WriteLine("Testing with arguments :");
			Console.WriteLine("Repository: " + defaultRepoUrl);
			Console.WriteLine("Destination Folder: " + defaultDestPath);

			GhostRepository ghostRepository = new GhostRepository(defaultRepoUrl, defaultDestPath);
			Console.WriteLine("Repository cloned");

			ghostRepository.CheckoutFile(defaultFileRelativePath);
			Console.WriteLine(defaultFileRelativePath + " checked out?");

			ghostRepository.CommitToRepository(defaultFileRealPath, defaultFileRelativePath);
			ghostRepository.PushFile();
			Console.WriteLine(defaultFileRelativePath + " pushed out?");

			ghostRepository.PrintGitStatus();
		}
	}
}
