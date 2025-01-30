using Microsoft.OpenApi.Models;
using MyCar.Context.Configurations;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MyCarContext>(
    builder.Configuration.GetSection("MyCarDatabase"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type => type.FullName);
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MY CAR API",
        Description = "API de gerenciamento de controle e manuteção veicular.",
        Contact = new OpenApiContact
        {
            Name = "MY CAR",
            Email = "welderfox@hotmail.com"
        },
        License = new OpenApiLicense
        {
            Name = "Todos os direitos reservados."
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(c =>
{
    c.DocExpansion(DocExpansion.List);
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
