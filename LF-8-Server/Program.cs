using Microsoft.AspNetCore.Builder;
using LF_8_Server.RequestHandlers;
using LF_8_Server.JsonTypes;
using LF_8_Server;

var builder = WebApplication.CreateBuilder(args);

//test local client
ServerStore.Clients.Add("localhost", new("http://127.0.0.1:8080/"));

var app = builder.Build();

GetDataHandler getDataHandler = new();

app.MapPost("/get-data", (GetDataRequest request) =>
{
	return getDataHandler.HandleRequest(request);
});

app.Run("http://0.0.0.0:8000");
