using Microsoft.AspNetCore.Http;
using LF_8_Server.JsonTypes;
using LF_8_Server.Utils;

namespace LF_8_Server.RequestHandlers
{
	internal class AddUserHandler : Handler<AddUserRequest>
	{
		public override IResult HandleRequest(AddUserRequest data)
		{
			if (data == null || data.Username == null || data.Username == "" || data.Password == null || data.Password == "")
			{
				return Results.BadRequest(new
				{
					Success = false,
					Message = "Data, Data.Username or Data.Password can not be null"
				});
			}

			foreach(var user in SaveManager.StoreInstance.Users)
			{
				if (user.Username == data.Username)
				{
					return Results.BadRequest(new
					{
						Success = false,
						Message = "User with this Username already exists"
					});
				}
			}

			SaveManager.StoreInstance.Users.Add(new UserEntry
			{
				Username = data.Username,
				HashedPassword = AuthUtils.Hash(data.Password),
				AuthToken = null
			});
			SaveManager.Save();
			return Results.Ok(new
			{
				Success = true
			});
		}
	}
}
