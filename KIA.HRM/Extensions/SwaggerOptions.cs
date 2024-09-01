using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace KIA.HRM.Extensions
{
    public static class SwaggerOptions
    {
        /// <summary>
        /// در قسمت بالای سواگر  Aurhorize جهت اضافه شدن دکمه   
        /// </summary>
        /// <param name="swagger"></param>
        public static void SwaggerGenOptions(SwaggerGenOptions swagger)//Action<SwaggerGenOptions> setupAction)
        {

            //This is to generate the Default UI of Swagger Documentation
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "JWT Token Authentication API",
                Description = ".NET 8 Web API"
            });
            // To Enable authorization using Swagger (JWT)
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
            });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
        }

        /// <summary>
        /// به دلیل هنگ کردن سواگر این کد اضافه شد تا هنگ نکند
        /// </summary>
        /// <param name="config"></param>
        public static void UseSwaggerUIOptions(SwaggerUIOptions config)
        {
            config.ConfigObject.AdditionalItems["syntaxHighlight"] = new Dictionary<string, object>
            {
                ["activated"] = false
            };
        }
    }
}
