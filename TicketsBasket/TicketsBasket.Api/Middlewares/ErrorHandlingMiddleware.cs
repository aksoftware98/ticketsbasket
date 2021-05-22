using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TicketsBasket.Infrastructure.Exceptions;
using TicketsBasket.Shared.Responses;

namespace TicketsBasket.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {

        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next; 
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, context); 
            }
        }

        private async Task HandleExceptionAsync(Exception ex, HttpContext context)
        {
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;
            string errorMessage = string.Empty;

            switch (ex)
            {
                case NotSupportedException:
                case BadRequestException:
                    errorMessage = ex.Message;
                    statusCode = HttpStatusCode.BadRequest; // 400 
                    break; 
                default:
                    errorMessage = "Something went wrong";
                    statusCode = HttpStatusCode.InternalServerError; // 500 
                    break;
            }

            var errorResponse = new OperationErrorRespnose(errorMessage);
            var contentAsString = JsonConvert.SerializeObject(errorResponse);

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(contentAsString); 
        }

    }
}
