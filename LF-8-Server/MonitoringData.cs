using Newtonsoft.Json;

namespace LF_8_Server
{
	internal class MemoryUsage
	{
		public float Used { get; set; }
		public float Total { get; set; }
		public string Unit { get; set; }
	}

	internal class DiskIO
	{
		[JsonProperty("readMBps")]
		public float CurrentRead { get; set; }
		[JsonProperty("writeMBps")]
		public float CurrentWrite { get; set; }
	}

	internal class DiskUsage
	{
		public string Name { get; set; }
		[JsonProperty("usedGiB")]
		public float TotalUsed { get; set; }
		[JsonProperty("totalGiB")]
		public float Total { get; set; }
	}

	internal class NetworkInterface
	{
		[JsonProperty("interface")]
		public string Name { get; set; }
		[JsonProperty("downloadMbit")]
		public float CurrentDownload { get; set; }
		[JsonProperty("uploadMbit")]
		public float CurrentUpload { get; set; }
	}

	internal class MonitoringData
	{
		public string Timestamp { get; set; }
		[JsonProperty("ram")]
		public MemoryUsage Memory { get; set; }
		public DiskIO DiskIO { get; set; }
		public DiskUsage[] Disks { get; set; }
		[JsonProperty("network")]
		public NetworkInterface[] NetworkInterfaces { get; set; }
	}
}
