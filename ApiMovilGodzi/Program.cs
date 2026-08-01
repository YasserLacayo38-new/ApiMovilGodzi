using ApiMovilGodzi.Conexion;
using ApiMovilGodzi.Models;
using ApiMovilGodzi.Repository;
using ApiMovilGodzi.UseCases;
using Microsoft.AspNetCore.Mvc;
using PruebaSuper_Aplication;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Development", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});



builder.Services.AddScoped<ConnectionString>(sp => 
    new ConnectionString(builder.Configuration.GetConnectionString("DefaultConnection") ?? "")
    )
    .AddScoped<OperationRepository>()
    .AddScoped<ListaPrecioRepository>()
    .AddScoped<ModeloRepository>()
    .AddScoped<VendedorRepository>()
    .AddScoped<OperationUseCase>()
    .AddScoped<ListaPrecioUseCase>()
    .AddScoped<ModeloUseCase>()
    .AddScoped<VendedorUseCase>();

var app = builder.Build();

app.UseCors("Development");

var operationApi = app.MapGroup("/operations");
operationApi.MapPost("/listaprecio", async (OperationUseCase useCase) =>
{
    var result = await useCase.SyncListaPrecioAync();
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});
operationApi.MapPost("/clientes", async ([FromBody]ClientesRequest request, OperationUseCase useCase) =>
{
    var result = await useCase.SyncClientesAsync(request.Ip);
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});
operationApi.MapPost("/vendedores", async (OperationUseCase useCase) =>
{
    var result = await useCase.SyncVendedoresAsync();
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});
operationApi.MapPost("/modelos", async (OperationUseCase useCase) =>
{
    var result = await useCase.SyncModelosAsync();
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});
operationApi.MapPost("/remisiones", async ([FromBody] RemisionRequest request, OperationUseCase useCase) =>
{
    var result = await useCase.SyncRemisionesAsync(request.FechaRemision, request.Ip);
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});
app.Run();

public record ClientesRequest(string Ip);
public record RemisionRequest(DateTime FechaRemision, string Ip)

[JsonSerializable(typeof(ClientesRequest))]
[JsonSerializable(typeof(Result<ResultProcedure>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
    
}
