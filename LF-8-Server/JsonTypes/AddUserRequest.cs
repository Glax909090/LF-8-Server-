namespace LF_8_Server.JsonTypes
{
	internal class AddUserRequest
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string? AuthToken { get; set; }
	}
}
