using ApiMovilGodzi.Conexion;
using ApiMovilGodzi.Models;
using ApiMovilGodzi.Models.ModelsDto;
using ApiMovilGodzi.Repository;
using ApiMovilGodzi.UseCases;
using Microsoft.AspNetCore.Mvc;
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
    .AddScoped<VendedoresNumProformaRepository>()
    .AddScoped<GarantiaRepository>()
    .AddScoped<ProformaRepository>()
    .AddScoped<LineaRepository>()
    .AddScoped<InventarioRepository>()
    .AddScoped<OperationUseCase>()
    .AddScoped<ListaPrecioUseCase>()
    .AddScoped<ModeloUseCase>()
    .AddScoped<VendedorUseCase>()
    .AddScoped<RemisionUseCase>()
    .AddScoped<ClienteUseCase>()
    .AddScoped<VendedoresNumProformaUseCase>()
    .AddScoped<GarantiaUseCase>()
    .AddScoped<ProformaUseCase>()
    .AddScoped<LineaUseCase>()
    .AddScoped<InventarioUseCase>();

//builder.WebHost.UseKestrelHttpsConfiguration();

var app = builder.Build();

app.UseCors("Development");

var operationApi = app.MapGroup("/operations");
operationApi.MapPost("/listaprecio", async (OperationUseCase useCase) =>
{
    var result = await useCase.SyncListaPrecioAync();
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});
operationApi.MapPost("/lineas", async (OperationUseCase useCase) =>
{
    var result = await useCase.SyncLineasAsync();
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
modelosApi.MapGet("/byvendedor", async ( [FromQuery] string ip, ModeloUseCase useCase) =>
{
    var result = await useCase.GetModelosByIp(ip);
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

var numProformasApi = app.MapGroup("/numproformas");
numProformasApi.MapGet("/", async (VendedoresNumProformaUseCase useCase) =>
{
    var result = await useCase.GetAllNumProformasAsync();
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});
numProformasApi.MapGet("/byip", async ([FromQuery] string ip, VendedoresNumProformaUseCase useCase) =>
{
    var result = await useCase.GetNumProformasByIpAsync(ip);
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});

var garantiasApi = app.MapGroup("/garantias");
garantiasApi.MapGet("/", async (GarantiaUseCase useCase) =>
{
    var result = await useCase.GetAllGarantiasAsync();
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});
garantiasApi.MapGet("/byvendedor", async ([FromQuery] DateTime fechaRemision, [FromQuery] string ip, GarantiaUseCase useCase) =>
{
    var result = await useCase.GetGarantiasByIpAndFechaAsync(ip, fechaRemision);
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});

var proformasApi = app.MapGroup("/proformas");
proformasApi.MapPost("/", async ([FromBody] ProformaDetalleGarantiaDTO request, ProformaUseCase useCase) =>
{
    var result = await useCase.AddProformasAsync(request);
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});

var lineasApi = app.MapGroup("/lineas");
lineasApi.MapGet("/", async (LineaUseCase useCase) =>
{
    var result = await useCase.GetAllLineasAsync();
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});

var inventarioApi = app.MapGroup("/inventario");
inventarioApi.MapGet("/byip", async ([FromQuery] string ip, InventarioUseCase useCase) =>
{
    var result = await useCase.GetInventarioByIpAsync(ip);
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
[JsonSerializable(typeof(Result<IEnumerable<VendedoresNumProforma>>))]
[JsonSerializable(typeof(Result<IEnumerable<Garantia>>))]
[JsonSerializable(typeof(ProformaDetalleGarantiaDTO))]
[JsonSerializable(typeof(Result<bool>))]
[JsonSerializable(typeof(Result<IEnumerable<Linea>>))]
[JsonSerializable(typeof(Result<IEnumerable<Inventario>>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
    
}