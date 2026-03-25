using LF_8_Server.JsonTypes;
using Microsoft.AspNetCore.Http;

namespace LF_8_Server.RequestHandlers
{
	internal class GetDataHandler : Handler<GetDataRequest>
	{
		public override IResult HandleRequest(GetDataRequest requestData)
		{
			Dictionary<string, MonitoringData> clientResponses = [];

			if (requestData.Client != null && ServerStore.Clients.TryGetValue(requestData.Client, out MonitoredClient? client) && client != null)
			{
				client.UpdateData();
				clientResponses.Add(requestData.Client, client.CurrentData);
			} else if (requestData.Client == null)
			{
				foreach(var kvp in ServerStore.Clients)
				{
					kvp.Value.UpdateData();
					clientResponses.Add(kvp.Key, kvp.Value.CurrentData);
				}
			}
			return Results.Ok(clientResponses);
		}
	}
}
