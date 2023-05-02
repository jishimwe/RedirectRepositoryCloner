// See https://aka.ms/new-console-template for more information
// See https://aka.ms/new-console-template for more information

namespace RedirectRepositoryCloner
{
	public class Program
	{

		static void Main(string[] args)
		{

			if (args.Length <= 2)
			{
				Console.WriteLine("No arguments");
				PrintUsage();
				return;
			}

			string repoUrl = "", destPath = "", redirUrl = "", redirPath = "", userName = "", branchName = "master", tokenPath = "";

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

					case "-u":
						userName = args[i + 1];
						break;

					case "-b": 
						branchName = args[i + 1];
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

			if (repoUrl == "" || destPath == "" || redirPath == "" || redirUrl == "" || userName == "" || tokenPath == "")
			{
				Console.WriteLine("Invalid arguments");
				PrintUsage();
				Console.WriteLine("Terminating process");
				return;
			}

			_ = new GhostRepository(repoUrl, destPath, userName, tokenPath, branchName);
			_ = new GhostRepository(redirUrl, redirPath, userName, tokenPath, branchName, true);

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
				"\n -u <user>			 : the git user username" +
				"\n -b <branch>			 : the name of the branch (optional with default value being \"master\")" +
				"\n -t <Path to token>   : The path to the file containing a git token");
			Console.WriteLine();
		}
	}
}
