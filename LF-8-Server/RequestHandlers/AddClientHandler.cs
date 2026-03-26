using LF_8_Server.JsonTypes;
using LF_8_Server.Utils;
using Microsoft.AspNetCore.Http;

namespace LF_8_Server.RequestHandlers
{
	internal class AddClientHandler : Handler<ClientRequest>
	{
		public override IResult HandleRequest(ClientRequest data)
		{
			if (data != null && data.Hostname != null && data.Url != null)
			{
				SaveManager.StoreInstance.Clients.Add(data.Hostname, new(data.Url));
				SaveManager.Save();
				return Results.Ok(new
				{
					success = true
				});
			}
			return Results.BadRequest(new
			{
				success = false,
				message = "Data, Data.Hostname and Data.Url can not be null"
			});
		}
	}
}
