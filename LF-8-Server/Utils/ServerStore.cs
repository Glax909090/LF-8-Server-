using Newtonsoft.Json;

namespace LF_8_Server.Utils
{
	internal class ServerStore
	{
		public Dictionary<string, MonitoredClient> Clients = [];
	}

	internal class SaveManager
	{
		public static ServerStore StoreInstance = new();

		private static readonly string _savePath = "server_data.json";

		public static void Save()
		{
			File.WriteAllText(_savePath, JsonConvert.SerializeObject(StoreInstance));
		}

		public static void Load()
		{
			if (!File.Exists(_savePath))
			{
				return;
			}

			StoreInstance = JsonConvert.DeserializeObject<ServerStore>(File.ReadAllText(_savePath))!;
		}
	}
}
