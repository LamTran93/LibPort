using LibPort.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LibPort.Middlewares.ExceptionHandlers
{
    public class ExceptionHandler : IExceptionHandler
    {
        public ExceptionHandler() { }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails();

            switch (exception)
            {
                case NotFoundException:
                    problemDetails.Status = StatusCodes.Status404NotFound;
                    problemDetails.Title = "Not Found";
                    problemDetails.Detail = exception.Message;
                    break;
                case NotValidIdException:
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "Invalid id";
                    problemDetails.Detail = exception.Message;
                    break;
                case TokenInvalidException:
                    problemDetails.Status = StatusCodes.Status401Unauthorized;
                    problemDetails.Title = "Invalid token";
                    problemDetails.Detail = exception.Message;
                    break;
                case NotEnoughBookException:
                    problemDetails.Status = StatusCodes.Status409Conflict;
                    problemDetails.Title = "Not enough book";
                    problemDetails.Detail = exception.Message;
                    break;
                case DataConflictException:
                    problemDetails.Status = StatusCodes.Status409Conflict;
                    problemDetails.Title = "Data conflict";
                    problemDetails.Detail = exception.Message;
                    break;
                default:
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    problemDetails.Title = "Internal Server Error";
                    problemDetails.Detail = exception.Message;
                    break;
            }

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
