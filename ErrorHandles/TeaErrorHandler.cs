using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;


namespace WebAppIdenty.ErrorHandles
{
    public class TeaErrorHandler
    {
        private readonly RequestDelegate _next;

        public TeaErrorHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
                if (context.Response.StatusCode == 404)  //page not found
                {
                    await HandleFor404Async(context);
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex); //internal errors
            }

        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = 500;

            if (IsRequestApi(context)) 
            {
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    State = 500,

                    message =ex.Message

                }));
            }
            else  //or use general
            {
                //when request page
                context.Response.Redirect("/Home/ErrorFor500");
            }

        }


        private static bool IsRequestApi(HttpContext context)
        {
            return context.Request.Path.Value.ToLower().StartsWith("/api");
        }


        private static async Task HandleFor404Async(HttpContext context)
        {
            if (IsRequestApi(context))
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    State = 404,
                    message = "the address is not find"
                }));
            }
            else
            {
                //when request page
                context.Response.Redirect("/Home/ErrorFor404");
            }
        }
    }
}
