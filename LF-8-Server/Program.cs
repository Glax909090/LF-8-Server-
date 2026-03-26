using Microsoft.AspNetCore.Builder;
using LF_8_Server.RequestHandlers;
using LF_8_Server.JsonTypes;
using LF_8_Server.Utils;

var builder = WebApplication.CreateBuilder(args);

SaveManager.Load();

ValueUpdater.StartUpdating();

var app = builder.Build();

GetDataHandler getDataHandler = new();
AddClientHandler addClientHandler = new();
DeleteClientHandler deleteClientHandler = new();

app.MapPost("/get-data", (GetDataRequest request) =>
{
	return getDataHandler.HandleRequest(request);
});

app.MapPost("/add-client", (ClientRequest request) =>
{
	return addClientHandler.HandleRequest(request);
});

app.MapPost("/delete-client", (ClientRequest request) =>
{
	return deleteClientHandler.HandleRequest(request);
});

app.Run("http://0.0.0.0:8000");
