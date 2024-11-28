using IPASSData.Models;

using IPASSDemo.Extensions;

using Microsoft.EntityFrameworkCore;

using Serilog;

using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((HostBuilderContext hostingContext, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
{
    //使用appsetting
    loggerConfiguration.ReadFrom.Configuration(builder.Configuration);
});
// Add services to the container.
builder.Services.AddCors(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

//聯繫DB
builder.Services.AddDbContext<IPassDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DB"),
                                                         b => b.MigrationsAssembly("IPASSData")));
//註冊Service
builder.Services.AddAPIService();
// 註冊驗證
builder.Services.AddCertified(builder.Configuration);
// 註冊授權
builder.Services.AddJwtAuthorization();
// 註冊 Mapper 設定
builder.Services.AddAutoMapper(Assembly.Load("IPASSData"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 啟用CORS策略
app.UseCors("AllowedCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
