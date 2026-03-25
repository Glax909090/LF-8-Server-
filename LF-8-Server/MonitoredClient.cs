using LF_8_Server.JsonTypes;
using Newtonsoft.Json;
using RestSharp;

namespace LF_8_Server
{
	internal class MonitoredClient(string url)
	{
		private RestClient _client = new(url);
		RestRequest _updateRequest = new("/stats", Method.Get);
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
