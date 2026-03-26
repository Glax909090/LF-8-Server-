using Microsoft.AspNetCore.Http;
using LF_8_Server.JsonTypes;
using LF_8_Server.Utils;

namespace LF_8_Server.RequestHandlers
{
	internal class GetDataHandler : Handler<GetDataRequest>
	{
		public override IResult HandleRequest(GetDataRequest requestData)
		{
			Dictionary<string, MonitoringData> clientResponses = [];

			if (requestData.Client != null && SaveManager.StoreInstance.Clients.TryGetValue(requestData.Client, out MonitoredClient? client) && client != null)
			{
				clientResponses.Add(requestData.Client, client.CurrentData);
			} else if (requestData.Client == null)
			{
				foreach(var kvp in SaveManager.StoreInstance.Clients)
				{
					clientResponses.Add(kvp.Key, kvp.Value.CurrentData);
				}
			}
			return Results.Ok(clientResponses);
		}
	}
}
