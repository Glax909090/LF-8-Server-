using LF_8_Server.JsonTypes;
using LF_8_Server.Utils;
using Microsoft.AspNetCore.Http;

namespace LF_8_Server.RequestHandlers
{
	internal class GetThresholdHandler : Handler<GetThresholdRequest>
	{
		public override IResult HandleRequest(GetThresholdRequest data)
		{
			if (data == null)
			{
				return Results.BadRequest(new
				{
					Success = false,
					Message = "Data can not be null"
				});
			}

			Dictionary<string, ThresholdData> clientResponses = [];

			if (data.Hostname == null)
			{
				foreach(var client in SaveManager.StoreInstance.Clients)
				{
					clientResponses.Add(client.Key, new ThresholdData
					{
						CpuLimit = client.Value.MaxCpu,
						RamLimit = client.Value.MaxRam,
						DiskLimit = client.Value.MaxDisk,
					});
				}
			} else if (SaveManager.StoreInstance.Clients.TryGetValue(data.Hostname, out var client) && client != null)
			{
				clientResponses.Add(data.Hostname, new ThresholdData
				{
					CpuLimit = client.MaxCpu,
					RamLimit = client.MaxRam,
					DiskLimit = client.MaxDisk,
				});
			}

			return Results.Ok(clientResponses);
		}
	}
}
