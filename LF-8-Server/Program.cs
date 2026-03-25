using LF_8_Server;

MonitoredClient client = new("localhost", "http://127.0.0.1:8080");

Console.WriteLine(client.CurrentData);

client.UpdateData();

Console.WriteLine(client.CurrentData);
