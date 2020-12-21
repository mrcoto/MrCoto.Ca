using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using MrCoto.Ca.Application.Common.Exceptions;
using MrCoto.Ca.Domain.Common.Exceptions;

namespace MrCoto.Ca.WebApi.Common.Exceptions
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandler(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                if (error is ValidationException validationException)
                {
                    await HandleValidationException(context, validationException);
                }
                else
                {
                    await HandleException(context, error);
                }
            }
        }

        private async Task HandleValidationException(HttpContext context, ValidationException exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int) HttpStatusCode.UnprocessableEntity;
            var code = exception.ExceptionCode;

            if (_env.IsDevelopment())
            {
                var trace = exception.StackTrace ?? "";
                var res = JsonSerializer.Serialize(new
                {
                    Code = code,
                    exception.Message,
                    exception.Errors,
                    Trace = trace
                });
                await response.WriteAsync(res);
            }
            else
            {
                var result = JsonSerializer.Serialize(new
                {
                    Code = code, 
                    Message = exception.Message,
                    Errors = exception.Errors,
                });
                await response.WriteAsync(result);
            }
        }

        private async Task HandleException(HttpContext context, Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = GetStatus(error);
            var code = GetCode(error);

            if (_env.IsDevelopment())
            {
                var trace = error.StackTrace ?? "";
                var res = JsonSerializer.Serialize(new {Code = code, Message = error?.Message, Trace = trace});
                await response.WriteAsync(res);
            }
            else
            {
                var result = JsonSerializer.Serialize(new {Code = code, Message = error?.Message});
                await response.WriteAsync(result);
            }
        }
        
        private string GetCode(Exception e)
        {
            if (e is BusinessException businessException) return businessException.ExceptionCode;
            if (e is KeyNotFoundException) return "SYS:KEY_NOT_FOUND_EXCEPTION";
            return "SYS:SERVER_ERROR";
        }

        private int GetStatus(Exception e)
        {
            if (e is BusinessException) return (int) HttpStatusCode.UnprocessableEntity;
            if (e is KeyNotFoundException) return (int) HttpStatusCode.NotFound;
            return (int) HttpStatusCode.InternalServerError;
        }
    }
}