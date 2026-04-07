using LF_8_Server.JsonTypes;
using LF_8_Server.Utils;
using Newtonsoft.Json;
using RestSharp;

namespace LF_8_Server
{
	[method: JsonConstructor]
	internal class MonitoredClient(string url, string name, float? maxCpu, float? maxRam, float? maxDisk)
	{
		public string Url { get; } = url;
		public string Hostname = name;

		public float MaxCpu { get; set; } = maxCpu ?? 200;
		[JsonIgnore]
		public bool HasCpuAlert { get; set; } = false;
		public float MaxRam { get; set; } = maxRam ?? 200;
		[JsonIgnore]
		public bool HasRamAlert { get; set; } = false;
		public float MaxDisk { get; set; } = maxDisk ?? 200;
		[JsonIgnore]
		public bool HasDiskAlert { get; set; } = false;

		[JsonIgnore]
		private readonly RestClient _client = new(url);
		[JsonIgnore]
		private readonly RestRequest _updateRequest = new("/stats", Method.Get);
		[JsonIgnore]
		public MonitoringData CurrentData = new();

		public void UpdateData()
		{
			var response = _client.Execute(_updateRequest);
			if (response.ErrorMessage == null && response.StatusCode == System.Net.HttpStatusCode.OK && response.Content != null)
			{
				var result = JsonConvert.DeserializeObject<MonitoringData>(response.Content);
				if (result != null)
				{
					CurrentData = result;
					if (CurrentData.Cpu > MaxCpu && !HasCpuAlert)
					{
						HasCpuAlert = true;
						MailUtils.SendMail($"CPU Alert for Client {Hostname} (eom)", "");
					} else if (CurrentData.Cpu < MaxCpu && HasCpuAlert)
					{
						HasCpuAlert = false;
						MailUtils.SendMail($"CPU Alert for Client {Hostname} cleared (eom)", "");
					}

					float memoryPercent = (CurrentData.Memory.Used / CurrentData.Memory.Total) * 100;
					Console.WriteLine(memoryPercent + " memory");
					if (memoryPercent > MaxRam && !HasRamAlert)
					{
						HasRamAlert = true;
						MailUtils.SendMail($"Ram Alert for Client {Hostname} (eom)", "");
					} else if (memoryPercent < MaxRam && HasRamAlert)
					{
						HasRamAlert = false;
						MailUtils.SendMail($"Ram Alert for Client {Hostname} cleared (eom)", "");
					}

					bool isDiskOver = false;

					foreach(var disk in CurrentData.Disks)
					{
						float diskPercent = (disk.TotalUsed /  disk.Total) * 100;
						if (diskPercent > MaxDisk)
						{
							isDiskOver = true;
						}
					}

					if (isDiskOver && !HasDiskAlert)
					{
						HasDiskAlert = true;
						MailUtils.SendMail($"Disk Alert for Client {Hostname} (eom)", "");
					} else if (!isDiskOver && HasDiskAlert)
					{
						HasDiskAlert = false;
						MailUtils.SendMail($"Disk Alert for Client {Hostname} cleared (eom)", "");
					}
				} else
				{
					CurrentData = new();
				}
			} else
			{
				CurrentData = new();
			}
		}
	}
}
