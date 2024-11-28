using IPASSData.Models;

using IPASSDemo.Extensions;

using Microsoft.EntityFrameworkCore;

using Serilog;

using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((HostBuilderContext hostingContext, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
{
    //�ϥ�appsetting
    loggerConfiguration.ReadFrom.Configuration(builder.Configuration);
});
// Add services to the container.
builder.Services.AddCors(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

//�pôDB
builder.Services.AddDbContext<IPassDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DB"),
                                                         b => b.MigrationsAssembly("IPASSData")));
//���UService
builder.Services.AddAPIService();
// ���U����
builder.Services.AddCertified(builder.Configuration);
// ���U���v
builder.Services.AddJwtAuthorization();
// ���U Mapper �]�w
builder.Services.AddAutoMapper(Assembly.Load("IPASSData"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// �ҥ�CORS����
app.UseCors("AllowedCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
