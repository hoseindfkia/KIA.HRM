using Azure.Core;
using DomainClass;
using DomainClass.Mongo;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;
using Service.Mongo;
using System.Text;
using System.Text.Json;
using ViewModel.WorkReport.Leave;

namespace KIA.HRM.Middleware;

public class ExceptionLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionLoggingMiddleware> _logger;
    private readonly IMongoService _mongoService;



    public ExceptionLoggingMiddleware(RequestDelegate next, ILogger<ExceptionLoggingMiddleware> logger, IMongoService mongoService)
    {
        _next = next;
        _logger = logger;
        _mongoService = mongoService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var ip = context.Connection.RemoteIpAddress?.ToString();
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            //TODO: بادی را باید دریافت کنم
            var BodyRequest = "";


            // Enable buffering to allow multiple reads  
            context.Request.EnableBuffering();

            // Read the request body  
            string requestBody;

            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                requestBody = await reader.ReadToEndAsync();
                // Reset the stream position to allow subsequent middlewares to read it  
                context.Request.Body.Position = 0;
            }


            //// Read the request body  
            //using var reader = new StreamReader(context.Request.Body);
            //var body = await reader.ReadToEndAsync();

            //// Deserialize to the model  
            //var model = JsonSerializer.Deserialize<LeavePostViewModel>(body);


            var userAgent = context.Request.Headers["User-Agent"];
            var userId = ((UserEntity?)context.Items["User"])?.Id;
            var path = context.Request.Path;
            var browser = context.Request.Headers["sec-ch-ua"];
            var platform = context.Request.Headers["sec-ch-ua-platform"];
            Share.Enum.HttpRequestType HttpRequestType = 0;
            if (context.Request.Method == "GET")
                HttpRequestType = Share.Enum.HttpRequestType.GET;
            if (context.Request.Method == "POST")
                HttpRequestType = Share.Enum.HttpRequestType.POST;
            if (context.Request.Method == "DELETE")
                HttpRequestType = Share.Enum.HttpRequestType.DELETE;
            var logEntity = new LoggerEntity()
            {
                Device = "",
                ExceptionMessage = ex.Message,
                UserId = userId,
                Ip = ip,
                Browser = browser,
                Path = path,
                Platform = platform + "--" + userAgent,
                CreateDateTime = DateTime.UtcNow,
                HttpRequestType = HttpRequestType,
                RequestBody = BodyRequest,
            };
            // یک لاگ در مونگو ذخیره می شود
            await _mongoService.CreateAsync(logEntity);

            // جهت تست  var mongoLogs = await _mongoService.GetAllAsync();

            #region نمایش خطا در خروجی
            // برای نمایش خطا در خروجی - کاربر عادی نباید خطا را مشاهده کند پس در قسمت تنظیمات باید میدلور جدا برای نمایش در خروجی بنویسم
            // Log the exception
            /// _logger.LogError(ex, "An error occurred.");
            #endregion

            // تنظیم خروجی خطا
            //context.Response = ( new JsonResult(new { message = "مجوز دسترسی وجود ندارد" }) { StatusCode = StatusCodes.Status401Unauthorized }).ToString();
            //return;

            // Rethrow the exception to pass it down the middleware pipeline
            throw;
        }
    }


}
//public class ExceptionLoggingMiddleware
//{
//    private readonly RequestDelegate _next;
//    private readonly ILogger _logger;

//    public ExceptionLoggingMiddleware(RequestDelegate next, ILogger logger)
//    {
//        _next = next;
//        _logger = logger;
//    }

//    public async Task InvokeAsync(HttpContext context)
//    {
//        try
//        {
//            await _next(context);
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError("An unhandled exception occurred", ex);
//            throw; // Rethrow the exception to be caught by the global exception handler
//        }
//    }
//}
