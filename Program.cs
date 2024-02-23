using API_CRUD.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Log.Logger=new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("log/client.txt",rollingInterval:RollingInterval.Day).CreateLogger();
////change the logger ans select the minimumlevel of
////debbuger we ca do .error .. we choose to log debug
////and we gonna write the log in log/client.txt and update the log every day
////then we finnish this with the methode CreateLogger() to create the log
////( we have installed two packages of log for all of this)

//builder.Host.UseSerilog();// Dependency injection
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));//dependency injection for dbContext and link it with my connection string

builder.Services.AddControllers(/*option =>{*/
    
//    option.ReturnHttpNotAcceptable = true;//Accept only json
//}
).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();//for output support json and xml
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
