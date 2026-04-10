namespace LF_8_Server.Utils
{
	internal class ValueUpdater
	{
		private static bool running = false;

		public static void StartUpdating()
		{
			if (running) return;
			running = true;
			new Thread(() =>
			{
				while (running)
				{
					foreach (var client in SaveManager.StoreInstance.Clients)
					{
						client.Value.UpdateData();
					}
					Thread.Sleep(1000);
				}
			}).Start();
		}

		public static void StopUpdating()
		{
			running = false;
		}
	}
}
