using EscuelaPrimaria;
using EscuelaPrimaria.Collection;
using EscuelaPrimaria.Service;
using EscuelaPrimaria.Service.NewFolder;
using EscuelaPrimaria.Service.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logPath = "logs";
if (!Directory.Exists(logPath))
{
    Directory.CreateDirectory(logPath);
}

// Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();

//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.DictionaryKeyPolicy = null;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SchoolContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IRespository<>), typeof(Respository<>));
builder.Services.AddScoped<IStudentRespository,StudentCollection>();
builder.Services.AddScoped<IAttendenceRespository, AttendenceCollection>();
builder.Services.AddScoped<IAttendenceService, AttendenceService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ILoggingService, LoggingService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.UseAuthorization();

app.MapControllers();

app.Run();
