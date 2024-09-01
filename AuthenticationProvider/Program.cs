using AuthenticationProvider.Authorization.ClaimBasedAuthorization;
using AuthenticationProvider.Authorization.ClaimBasedAuthorization.Attributes;
using AuthenticationProvider.Models.Context;
using AuthenticationProvider.Models.Main;
using AuthenticationProvider.Models.User;
using AuthenticationProvider.PersianTransalation;
using AuthenticationProvider.Security.Policy;
using AuthenticationProvider.Service.Message;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<UnitOfWorkContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddIdentity<UserEntity, IdentityRole>(option =>
  {
      // تنظیمات مربوط به پسورد و فیلد ها
      option.User.RequireUniqueEmail = true;
      option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // کاربر پس از قفل شدن به مدت 5 دقیقه به اکانت خود دسترسی ندارد
  })
  .AddEntityFrameworkStores<UnitOfWorkContext>()
  .AddDefaultTokenProviders()
  .AddErrorDescriber<PersianIdentityErrorDescriber>();


// Dependency Injections
builder.Services.AddSingleton<IMessageService, MessageService>();

// تنظیمات مربوط به پالیسی و کلیم در اینجا تعیین می شود
// در این قسمت می توانیم پالیسی های کاستوم را اضافه کنیم و با اضافه کردن هر کدام از این پالیسی ها به
// هر اکشن، می توانیم پالیسی های خود را اعمال کنیم
builder.Services.AddAuthorization(option => AuthorizationOptionsStartup.optios(option));

builder.Services.AddClaimBasedAuthorization();
// تنظیمات کلی برنامه
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Swagger Setting
// جهت اضافه شدن بخش اتورایط به سواگر جهت تست توکن
builder.Services.AddSwaggerGen(swagger =>
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
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // app.UseSwaggerUI();  سواگر هنگ می کرد مجبور شدم این کد را بزنم تا هنگ نکنه
    app.UseSwaggerUI(
       config =>
       {
           config.ConfigObject.AdditionalItems["syntaxHighlight"] = new Dictionary<string, object>
           {
               ["activated"] = false
           };
       }
   );
}


app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
