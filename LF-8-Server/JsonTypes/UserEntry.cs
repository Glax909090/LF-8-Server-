namespace LF_8_Server.JsonTypes
{
	internal class UserEntry
	{
		public string Username { get; set; }
		public string HashedPassword { get; set; }
		public string? AuthToken { get; set; }
	}
}
