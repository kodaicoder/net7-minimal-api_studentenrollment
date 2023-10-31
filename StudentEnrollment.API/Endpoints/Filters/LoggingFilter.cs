namespace StudentEnrollment.API.Endpoints.Filters
{
	public class LoggingFilter : IEndpointFilter
	{
		private readonly ILogger _logger;

		public LoggingFilter(ILoggerFactory loggerFactory)
		{
			this._logger = loggerFactory.CreateLogger<LoggingFilter>();
		}

		public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
		{
			string method = context.HttpContext.Request.Method;
			string path = context.HttpContext.Request.Path;

			_logger.LogInformation($"{method} request made to {path}");

			try
			{
				object? result = await next(context);
				_logger.LogInformation($"{method} request made to {path} completed with status code {context.HttpContext.Response.StatusCode}");
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"{method} request made to {path} failed with message : {ex.Message}");
				return Results.Problem("An Error has Occured, please try again later.");
			}
		}
	}
}
