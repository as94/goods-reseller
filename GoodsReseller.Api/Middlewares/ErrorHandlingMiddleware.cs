using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

namespace GoodsReseller.Api.Middlewares
{
    public sealed class ErrorHandlingMiddleware
	{
		private readonly ILogger<ErrorHandlingMiddleware> _logger;
		private readonly RequestDelegate _next;

		public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (ValidationException ex)
			{
				// ошибки валидации в синтаксисе запросов, повторять без изменений не нужно, 400
				var httpStatusCode = HttpStatusCode.BadRequest;
				LogException(ex, httpStatusCode, context.Request);
				WriteTextResponse(context, httpStatusCode, ex.Message);
			}
			catch (AuthenticationException ex)
			{
				// ошибки доступа 401
				var httpStatusCode = HttpStatusCode.Unauthorized;
				LogException(ex, httpStatusCode, context.Request);
				WriteTextResponse(context, httpStatusCode);
			}
			catch (SecurityException ex)
			{
				// ошибки доступа 403
				var httpStatusCode = HttpStatusCode.Forbidden;
				LogException(ex, httpStatusCode, context.Request);
				WriteTextResponse(context, httpStatusCode);
			}
			catch (Exception ex)
			{
				var httpStatusCode = HttpStatusCode.InternalServerError;
				LogException(ex, httpStatusCode, context.Request);
				WriteTextResponse(context, httpStatusCode, ex.Message);
			}
		}

		private async void WriteTextResponse(HttpContext context, HttpStatusCode httpCode, string message = "")
		{
			context.Response.ContentType = "text/plain";
			context.Response.StatusCode = (int) httpCode;

			await context.Response.WriteAsync(string.IsNullOrEmpty(message) ? string.Empty : message);
		}

		private void LogException(Exception exception, HttpStatusCode httpStatusCode, HttpRequest request)
		{
			var dumpRequest = DumpRequest(request);

			if ((int) httpStatusCode < (int) HttpStatusCode.InternalServerError)
			{
				_logger.LogWarning(exception, dumpRequest);
			}
			else
			{
				_logger.LogError(exception, dumpRequest);
			}
		}

		private static string DumpRequest(HttpRequest request)
		{
			var sb = new StringBuilder(1024);

			sb.AppendLine($"{request.Method} {request.GetDisplayUrl()}");
			sb.AppendLine("Headers:");
			foreach (var header in request.Headers)
			{
				sb.AppendLine($"{header.Key} : {header.Value}");
			}

			if (!request.HasFormContentType)
			{
				return sb.ToString();
			}

			sb.AppendLine("Form:");
			foreach (var pair in request.Form)
			{
				sb.AppendLine($"{pair.Key} : {pair.Value}");
			}

			return sb.ToString();
		}
	}
}