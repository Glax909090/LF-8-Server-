using LF_8_Server.JsonTypes;
using LF_8_Server.Utils;
using Microsoft.AspNetCore.Http;

namespace LF_8_Server.RequestHandlers
{
	internal class SetThresholdHandler : Handler<SetThresholdRequest>
	{
		public override IResult HandleRequest(SetThresholdRequest data)
		{
			if (data == null || data.Hostname == null)
			{
				return Results.BadRequest(new
				{
					Success = false,
					Message = "Data and Data.Hostname can not be null"
				});
			}

			if (SaveManager.StoreInstance.Clients.TryGetValue(data.Hostname, out var client) && client != null)
			{
				if (data.CpuLimit.HasValue)
				{
					client.MaxCpu = data.CpuLimit.Value;
					client.HasCpuAlert = false;
				}

				if (data.DiskLimit.HasValue)
				{
					client.MaxDisk = data.DiskLimit.Value;
					client.HasDiskAlert = false;
				}

				if (data.RamLimit.HasValue)
				{
					client.MaxRam = data.RamLimit.Value;
					client.HasRamAlert = false;
				}

				SaveManager.Save();

				return Results.Ok(new
				{
					Success = true
				});
			} else
			{
				return Results.BadRequest(new
				{
					Success = false,
					Message = "Client not found"
				});
			}
		}
	}
}
