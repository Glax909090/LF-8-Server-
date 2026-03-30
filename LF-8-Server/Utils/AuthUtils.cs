using LF_8_Server.JsonTypes;
using System.Security.Cryptography;
using System.Text;

namespace LF_8_Server.Utils
{
	internal class AuthUtils
	{
		public static string Hash(string input)
		{
			byte[] inputBytes = Encoding.UTF8.GetBytes(input);
			byte[] hashBytes = SHA512.HashData(inputBytes);

			string hashString = Convert.ToHexString(hashBytes);

			return hashString;
		}

		public static bool VerifyAuth(string? authToken)
		{
			if (authToken == null) return false;
			if (Hash(authToken) == SaveManager.StoreInstance.AdminToken) return true;

			foreach(var user in SaveManager.StoreInstance.Users)
			{
				if (user.AuthToken == Hash(authToken))
				{
					return true;
				}
			}

			return false;
		}

		public static string? AuthenticateUser(UserEntry user, string password)
		{
			string? authToken = null;
			if (user.HashedPassword == Hash(password))
			{
				authToken = Guid.NewGuid().ToString();
				user.AuthToken = Hash(authToken);
				SaveManager.Save();
			}
			return authToken;
		}

		public static UserEntry? FindUser(string username)
		{
			foreach (var entry in SaveManager.StoreInstance.Users)
			{
				if (entry.Username == username)
				{
					return entry;
				}
			}
			return null;
		}
	}
}
