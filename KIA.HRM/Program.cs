using DataLayer;
using DataLayer.MongoDB;
using DomainClass.Main;
using DomainClass.Mongo;
using KIA.HRM.Extensions;
using KIA.HRM.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        ///   options.Authority = "https://localhost:5018/"; // Set your Auth provider here. // برای گوگل استفاده می شود
        options.Audience = "https://localhost:5018/";        // Set your API audience here.  
    });

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<UnitOfWorkContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.Configure<LoggerDatabaseSettings>(builder.Configuration.GetSection("LoggerDatabase"));


builder.Services.AddSingleton<MongoDbContext>(provider =>
{
    var settings = provider.GetRequiredService<IOptions<LoggerDatabaseSettings>>().Value;
    return new MongoDbContext(settings.ConnectionString, settings.DatabaseName, settings.LoggerCollectionName);
});

// تنظیمات کلی برنامه
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppKiaSettings"));

//DependencyInjection
builder.Services.AddCustomServices();


builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(
          policy =>
          {
              policy.AllowAnyOrigin();
              policy.AllowAnyHeader();
              policy.AllowAnyMethod();
              //policy.WithOrigins("http://localhost:3000",
              //                   "http://localhost:3001/");
          }
        );
});

builder.Services.AddSwaggerGen(swagger => SwaggerOptions.SwaggerGenOptions(swagger));


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//app.UseWebRoot("path/to/your/new/wwwroot/");


// Logger Middleware
// در صورتی که بخواهم خطاها در  خروجی نمایش دهد باید تنظیمات در این متد تغییر بدهم
// ثبت خطا در مونگو نیز توسط این میدلویر ذخیره می شود
app.UseExceptionLogging();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config => SwaggerOptions.UseSwaggerUIOptions(config));

}




app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
