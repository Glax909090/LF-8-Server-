using Microsoft.AspNetCore.Http;
using LF_8_Server.JsonTypes;
using LF_8_Server.Utils;

namespace LF_8_Server.RequestHandlers
{
	internal class AuthRequestHandler : Handler<AuthRequest>
	{
		public override IResult HandleRequest(AuthRequest data)
		{
			if (data == null || data.Username == null || data.Password == null)
			{
				return Results.BadRequest(new AuthResponse
				{
					AuthToken = null,
					Success = false,
					Message = "Data, Username or Password can not be null."
				});
			}

			var user = AuthUtils.FindUser(data.Username);
			if (user == null)
			{
				return Results.BadRequest(new AuthResponse
				{
					AuthToken = null,
					Success = false,
					Message = "Incorrect Username or Password"
				});
			}

			if (AuthUtils.AuthenticateUser(user, data.Password) is string result)
			{
				return Results.Ok(new AuthResponse
				{
					AuthToken = result,
					Success = true,
					Message = null
				});
			} else
			{
				return Results.BadRequest(new AuthResponse
				{
					AuthToken = null,
					Success = false,
					Message = "Incorrect Username or Password"
				});
			}
		}
	}
}
