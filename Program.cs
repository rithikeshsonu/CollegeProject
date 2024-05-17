using CollegeProject.MyLogging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
/*
builder.Logging.ClearProviders(); //Removes all types of logging mechanisms from application
builder.Logging.AddConsole(); //Logs only to console
builder.Logging.AddDebug();//Logs only to debug
*/

//Using SeriLog
//logs to file - with minimum level as Information. Log will be generated with time gap as specified.(Minutes, days, Hours, etc.,)
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("Log/log.txt",rollingInterval: RollingInterval.Day)
    .CreateLogger();

//builder.Host.UseSerilog(); //Only to use Serilog by overriding existing loggers.
builder.Logging.AddSerilog(); // To add Serilog logging with other type of existing loggings as well.

// Add services to the container.
//Content Negotiation -> How to now allow other response types like xml, bla bla...
//Shows 406 Status Code -> Which means not acceptable data format when trying to return other types apart from JSON.
//builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true).AddNewtonsoftJson();
//Below Line is added to support XML data............
builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//For loosely coupled Dependency Injection - You will just have to make your changes at one place...
//Easier to maintain
builder.Services.AddScoped<IMyLoggerr, LogToFile>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//testing commits

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
