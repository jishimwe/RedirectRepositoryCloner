using LibGit2Sharp;

namespace RedirectRepositoryCloner
{
	internal class GhostRepository
	{
		private Repository repository;
		private string branchName, userName, tokenPath;



		public GhostRepository(string repoURL, string destPath, string userName, string tokenPath, string branchName, bool rdr = false)
		{
			this.userName = userName;
			this.tokenPath = tokenPath;
			this.branchName = branchName;
			repository = CloneRepository(repoURL, destPath, rdr);
		}

		private string GetToken(string filepath)
		{
			if (filepath == null || !File.Exists(filepath))
				return "";
			string s = File.ReadAllText(filepath);
			return s;
		}

		private Repository CloneRepository(string repoURL, string destPath, bool rdr = false)
		{
			// If the directory already exist, we reset it. Deleting it's content careful which path you give
			if (Directory.Exists(destPath))
			{
				Directory.Delete(destPath, true);
				Directory.CreateDirectory(destPath);
			}

			var co = new CloneOptions
			{
				BranchName = branchName,
				CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials { Username = userName, Password = GetToken(tokenPath) }
			};
			if (!rdr)
				co.Checkout = false;
			Repository.Clone(repoURL, destPath, co);

			return new Repository(destPath);
		}
	}
}