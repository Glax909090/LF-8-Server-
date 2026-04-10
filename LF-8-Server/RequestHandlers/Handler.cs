using Microsoft.AspNetCore.Http;

namespace LF_8_Server.RequestHandlers
{
	internal abstract class Handler<T>
	{
		public abstract IResult HandleRequest(T data);

		public IResult HandleRequest()
		{
			return Results.NotFound();
		}
	}
}
