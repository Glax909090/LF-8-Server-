using LF_8_Server.JsonTypes;
using Newtonsoft.Json;
using RestSharp;

namespace LF_8_Server
{
	[method: JsonConstructor]
	internal class MonitoredClient(string url)
	{
		public string Url { get; } = url;

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
