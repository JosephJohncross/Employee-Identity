using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EmployeeManagement.Infrastructure.Middelwares.GlobalExceptionHandlingMiddleware
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (IdentityNotFoundException e)
            {
                await GenerateProblemDetails(
                    e.Message,
                    (int)HttpStatusCode.NotFound,
                    "NotFound Error",
                    context
                );
            }
            catch (IdentityUserExistException e)
            {
                await GenerateProblemDetails(
                    e.Message,
                    (int)HttpStatusCode.BadRequest,
                    "Bad Request",
                    context
                );
            }
            catch (IdentityBadRequestException e)
            {
                await GenerateProblemDetails(
                    e.Message,
                    (int)HttpStatusCode.BadRequest,
                    "Bad Request Error",
                    context
                );
            }
            catch (IdentityUnAuthorizedException e)
            {
                await GenerateProblemDetails(
                    e.Message,
                    (int)HttpStatusCode.Unauthorized,
                    "Unauthorized Error",
                    context
                );
            }
            catch (IdentityForbiddenException e)
            {
                await GenerateProblemDetails(
                    e.Message,
                    (int)HttpStatusCode.Forbidden,
                    "Forbidden Error",
                    context
                );
            }
            catch (IdentityServiceNotFound e)
            {
                await GenerateProblemDetails(
                    e.Message,
                    (int)HttpStatusCode.InternalServerError,
                    "Internal server Error",
                    context
                );
            }
            catch (Exception e)
            {
                await GenerateProblemDetails(
                    e.Message,
                    (int)HttpStatusCode.InternalServerError,
                    "Internal server Error",
                    context
                );
            }
            
        }

        private async Task  GenerateProblemDetails(string errorMessage, int statusCode, string title, HttpContext context)
        {
            context.Response.StatusCode = statusCode;

            ProblemDetails problemDetails = new(){
                    Status = statusCode,
                    Title = title,
                    Detail = errorMessage,
            };

            string json = JsonSerializer.Serialize(problemDetails);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
    }
}