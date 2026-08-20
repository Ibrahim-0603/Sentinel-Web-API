using Microsoft.AspNetCore.Mvc;
using SentinelSystemApi.Api.Exceptions;

namespace SentinelSystemApi.Api.Middleware;

public class GlobalExceptionMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<GlobalExceptionMiddleware> _logger;
	public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (NotFoundException e)
		{
			await WriteProblemDetails(context, 404, "Not Found", e.Message);
		}
		catch (Exception e)
		{
			_logger.LogError(e, "Unhandled Exception");
			await WriteProblemDetails(context, 500, "An error occured", "Please contact support");
		}
	}
	private static async Task WriteProblemDetails(HttpContext ctx, int status, string title, string detail)
	{
		ctx.Response.StatusCode = status;
		ctx.Response.ContentType = "application/problem+json";
		var problem = new ProblemDetails
		{
			Status = status,
			Title = title,
			Detail = detail
		};
		await ctx.Response.WriteAsJsonAsync(problem);
	}
}