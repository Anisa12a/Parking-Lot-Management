using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using ParkingLotManagement.DbContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


 builder.Services.AddDbContext<ParkingInfoContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("Ef_Postgres_Db")));


builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>(); 


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
