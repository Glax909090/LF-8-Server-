using LF_8_Server;
using Newtonsoft.Json;
using RestSharp;

RestClient client = new("http://localhost:8080/");

RestRequest request = new("/stats", Method.Get);
var response = client.Execute(request);
var data = JsonConvert.DeserializeObject<MonitoringData>(response.Content!);
Console.WriteLine(data);
