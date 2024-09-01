using DomainClass;
using DomainClass.Mongo;
using Microsoft.AspNetCore.Mvc;
using Service.Mongo;

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
            var userAgent = context.Request.Headers["User-Agent"];
            var userId = ((UserEntity?)context.Items["User"])?.Id;
            var path = context.Request.Path;
            var browser = context.Request.Headers["sec-ch-ua"];
            var platform = context.Request.Headers["sec-ch-ua-platform"];

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
