using LF_8_Server.JsonTypes;
using LF_8_Server.Utils;
using Microsoft.AspNetCore.Http;

namespace LF_8_Server.RequestHandlers
{
	internal class DeleteClientHandler : Handler<ClientRequest>
	{
		public override IResult HandleRequest(ClientRequest data)
		{
			if (data == null || data.Url == null || data.Hostname == null)
			{
				return Results.BadRequest(new
				{
					success = false,
					message = "Data, Data.Url and Data.Hostname can not be null"
				});
			}

			if (!SaveManager.StoreInstance.Clients.Remove(data.Hostname))
			{
				return Results.BadRequest(new
				{
					success = false,
					message = "Client not Found"
				});
			} else
			{
				return Results.Ok(new
				{
					success = true
				});
			}
		}
	}
}
