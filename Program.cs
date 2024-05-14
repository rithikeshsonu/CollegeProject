var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Content Negotiation -> How to now allow other response types like xml, bla bla...
//Shows 406 Status Code -> Which means not acceptable data format when trying to return other types apart from JSON.
//builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true).AddNewtonsoftJson();
//Below Line is added to support XML data............
builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
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
//testing commits

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
