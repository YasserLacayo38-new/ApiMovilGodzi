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
    .AddScoped<RemisionRepository>()
    .AddScoped<VendedoresIPRepository>()
    .AddScoped<ClienteRepository>()
    .AddScoped<OperationUseCase>()
    .AddScoped<ListaPrecioUseCase>()
    .AddScoped<ModeloUseCase>()
    .AddScoped<VendedorUseCase>()
    .AddScoped<RemisionUseCase>()
    .AddScoped<ClienteUseCase>();

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

var listaprecioApi = app.MapGroup("/listaprecio");
listaprecioApi.MapGet("/", async (ListaPrecioUseCase useCase) =>
{
    var result = await useCase.GetAllListaPrecioAsync();
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});

var modelosApi = app.MapGroup("/modelos");
modelosApi.MapGet("/", async (ModeloUseCase useCase) =>
{
    var result = await useCase.GetAllModeloAsync();
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});
modelosApi.MapGet("/byvendedor", async ([FromQuery] DateTime fechaRemision, [FromQuery] string ip, ModeloUseCase useCase) =>
{
    var result = await useCase.GetModelosByIpAndFechaRemisionAsync(ip, fechaRemision);
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});

var remisionesApi = app.MapGroup("/remisiones");
remisionesApi.MapGet("/", async (RemisionUseCase useCase) =>
{
    var result = await useCase.GetAllRemisionAsync();
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});
remisionesApi.MapGet("/byvendedor", async ([FromQuery] DateTime fechaRemision, [FromQuery] string ip, RemisionUseCase useCase) =>
{
    var result = await useCase.GetAllRemisionByFechaAndIpAsync(fechaRemision, ip);
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});

var clientesApi = app.MapGroup("/clientes");
clientesApi.MapGet("/", async (ClienteUseCase useCase) =>
{
    var result = await useCase.GetAllClienteAsync();
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});

clientesApi.MapGet("/byip", async ([FromQuery] string ip, ClienteUseCase useCase) =>
{
    var result = await useCase.GetClientesByIpAsync(ip);
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});

var vendedoresApi = app.MapGroup("/vendedores");
vendedoresApi.MapGet("/byip", async ([FromQuery] string ip, VendedorUseCase useCase) =>
{
    var result = await useCase.GetVendedorByIpAsync(ip);
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});


app.Run();

public record ClientesRequest(string Ip);
public record RemisionRequest(DateTime FechaRemision, string Ip);

[JsonSerializable(typeof(ClientesRequest))]
[JsonSerializable(typeof(Result<ResultProcedure>))]
[JsonSerializable(typeof(RemisionRequest))]
[JsonSerializable(typeof(Result<IEnumerable<ListaPrecio>>))]
[JsonSerializable(typeof(Result<IEnumerable<Modelo>>))]
[JsonSerializable(typeof(Result<IEnumerable<Remision>>))]
[JsonSerializable(typeof(Result<IEnumerable<Cliente>>))]
[JsonSerializable(typeof(Result<Vendedor>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
    
}
