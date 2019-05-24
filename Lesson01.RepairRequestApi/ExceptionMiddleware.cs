using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Lesson01.RepairRequestApi
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.Clear();

                var result = JsonConvert.SerializeObject(new { message = ex.Message, error = ex });
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(result);
            }
        }
    }
}
