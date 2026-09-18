using ApiMovilGodzi.Conexion;
using ApiMovilGodzi.Models;
using ApiMovilGodzi.Models.ModelsDto;
using ApiMovilGodzi.Repository;
using ApiMovilGodzi.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWindowsService();

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
    .AddScoped<VendedoresDeviceRepository>()
    .AddScoped<ClienteRepository>()
    .AddScoped<VendedoresNumProformaRepository>()
    .AddScoped<GarantiaRepository>()
    .AddScoped<ProformaRepository>()
    .AddScoped<LineaRepository>()
    .AddScoped<InventarioRepository>()
    .AddScoped<AdminDeviceRepository>()
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
    .AddScoped<InventarioUseCase>()
    .AddScoped<AdminDeviceUseCase>();

builder.WebHost.UseKestrelHttpsConfiguration();

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
    var result = await useCase.SyncClientesAsync(request.IdDevice);
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
    var result = await useCase.SyncRemisionesAsync(request.FechaRemision, request.IdDevice);
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
modelosApi.MapGet("/bydevice", async ( [FromQuery] string idDevice, ModeloUseCase useCase) =>
{
    var result = await useCase.GetModelosByDevice(idDevice);
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});

var remisionesApi = app.MapGroup("/remisiones");
remisionesApi.MapGet("/", async (RemisionUseCase useCase) =>
{
    var result = await useCase.GetAllRemisionAsync();
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});
remisionesApi.MapGet("/bydevice", async ([FromQuery] DateTime fechaRemision, [FromQuery] string idDevice, RemisionUseCase useCase) =>
{
    var result = await useCase.GetAllRemisionByFechaAndDeviceAsync(fechaRemision, idDevice);
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});

var clientesApi = app.MapGroup("/clientes");
clientesApi.MapGet("/", async (ClienteUseCase useCase) =>
{
    var result = await useCase.GetAllClienteAsync();
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});

clientesApi.MapGet("/bydevice", async ([FromQuery] string idDevice, ClienteUseCase useCase) =>
{
    var result = await useCase.GetClientesByDeviceAsync(idDevice);
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});

var vendedoresApi = app.MapGroup("/vendedores");
vendedoresApi.MapGet("/", async(VendedorUseCase useCase) =>
{
    var result = await useCase.GetAllVendedorAsync();
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});
vendedoresApi.MapGet("/bydevice", async ([FromQuery] string idDevice, VendedorUseCase useCase) =>
{
    var result = await useCase.GetVendedorByDeviceAsync(idDevice);
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});

var numProformasApi = app.MapGroup("/numproformas");
numProformasApi.MapGet("/", async (VendedoresNumProformaUseCase useCase) =>
{
    var result = await useCase.GetAllNumProformasAsync();
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});
numProformasApi.MapGet("/bydevice", async ([FromQuery] string idDevice, VendedoresNumProformaUseCase useCase) =>
{
    var result = await useCase.GetNumProformasByDeviceAsync(idDevice);
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});
numProformasApi.MapPost("/", async ([FromBody] NumProformaRequest request, VendedoresNumProformaUseCase useCase) =>
{
    var result = await useCase.AddNumProformaAsync(request.NumeroProforma, request.CodigoVendedor);
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});

var garantiasApi = app.MapGroup("/garantias");
garantiasApi.MapGet("/", async (GarantiaUseCase useCase) =>
{
    var result = await useCase.GetAllGarantiasAsync();
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});
garantiasApi.MapGet("/bydevice", async ([FromQuery] string idDevice, GarantiaUseCase useCase) =>
{
    var result = await useCase.GetGarantiasDisponiblesByDeviceAsync(idDevice);
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
inventarioApi.MapGet("/bydevice", async ([FromQuery] string idDevice, InventarioUseCase useCase) =>
{
    var result = await useCase.GetInventarioByDeviceAsync(idDevice);
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});

var adminDevicesApi = app.MapGroup("/admindevices");
adminDevicesApi.MapGet("/", async (AdminDeviceUseCase useCase) =>
{
    var result = await useCase.GetAllAdminDevicesAsync();
    return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
});

app.Run();

public record ClientesRequest(string IdDevice);
public record RemisionRequest(DateTime FechaRemision, string IdDevice);
public record NumProformaRequest(int NumeroProforma, string CodigoVendedor);

[JsonSerializable(typeof(ClientesRequest))]
[JsonSerializable(typeof(Result<ResultProcedure>))]
[JsonSerializable(typeof(RemisionRequest))]
[JsonSerializable(typeof(NumProformaRequest))]
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
[JsonSerializable(typeof(Result<IEnumerable<AdminDevice>>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
    
}