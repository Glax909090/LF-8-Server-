using Microsoft.AspNetCore.Builder;
using LF_8_Server.RequestHandlers;
using Microsoft.AspNetCore.Http;
using LF_8_Server.JsonTypes;
using LF_8_Server.Utils;

var builder = WebApplication.CreateBuilder(args);

SaveManager.Load();

ValueUpdater.StartUpdating();

var app = builder.Build();

GetDataHandler getDataHandler = new();
AddUserHandler addUserHandler = new();
AddClientHandler addClientHandler = new();
AuthRequestHandler authRequestHandler = new();
DeleteClientHandler deleteClientHandler = new();
SetThresholdHandler setThresholdHandler = new();
GetThresholdHandler getThresholdHandler = new();

LogUtils.Initialize();

app.MapPost("/auth", (AuthRequest request) =>
{
	return authRequestHandler.HandleRequest(request);
});

app.MapPost("/add-user", (AddUserRequest request) =>
{
	if (!AuthUtils.VerifyAuth(request.AuthToken))
	{
		return Results.BadRequest(new
		{
			Success = false,
			Message = "Invalid Auth Token"
		});
	}
	return addUserHandler.HandleRequest(request);
});

app.MapPost("/get-data", (GetDataRequest request) =>
{
	if (!AuthUtils.VerifyAuth(request.AuthToken))
	{
		return Results.BadRequest(new
		{
			Success = false,
			Message = "Invalid Auth Token"
		});
	}
	return getDataHandler.HandleRequest(request);
});

app.MapPost("/get-threshold", (GetThresholdRequest request) =>
{
	if (!AuthUtils.VerifyAuth(request.AuthToken))
	{
		return Results.BadRequest(new
		{
			Success = false,
			Message = "Invalid Auth Token"
		});
	}
	return getThresholdHandler.HandleRequest(request);
});

app.MapPost("/set-threshold", (SetThresholdRequest request) =>
{
	if (!AuthUtils.VerifyAuth(request.AuthToken))
	{
		return Results.BadRequest(new
		{
			Success = false,
			Message = "Invalid Auth Token"
		});
	}
	return setThresholdHandler.HandleRequest(request);
});

app.MapPost("/add-client", (ClientRequest request) =>
{
	if (!AuthUtils.VerifyAuth(request.AuthToken))
	{
		return Results.BadRequest(new
		{
			Success = false,
			Message = "Invalid Auth Token"
		});
	}
	return addClientHandler.HandleRequest(request);
});

app.MapPost("/delete-client", (ClientRequest request) =>
{
	if (!AuthUtils.VerifyAuth(request.AuthToken))
	{
		return Results.BadRequest(new
		{
			Success = false,
			Message = "Invalid Auth Token"
		});
	}
	return deleteClientHandler.HandleRequest(request);
});

app.Run("http://0.0.0.0:8000");
