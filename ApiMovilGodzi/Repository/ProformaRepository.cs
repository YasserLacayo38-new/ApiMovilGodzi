using ApiMovilGodzi.Conexion;
using ApiMovilGodzi.Models;
using ApiMovilGodzi.Models.ModelsDto;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Security.Permissions;

namespace ApiMovilGodzi.Repository;

public class ProformaRepository
{
    private readonly ConnectionString _connectionString;
    public ProformaRepository(ConnectionString connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task AddProformasListAsync(ProformaDetalleGarantiaDTO dto)
    {
        using var connection = new SqlConnection(_connectionString.Connection);
        await connection.OpenAsync();
        using var transaction = connection.BeginTransaction();

        try
        {
            const string insertProforma = @"
                INSERT INTO Proforma (idProforma, NumeroProforma, codigoCliente, codigoVendedor,
                                      codigoListaPrecio, tipoIva, tipo, fechaVenta, ventaBruta,
                                      NumeroTransferencia, Observacion, descuento, ventaTot,
                                      sincronizada, valida)
                VALUES (@IdProforma, @NumeroProforma, @CodigoCliente, @CodigoVendedor,
                        @CodigoListaPrecio, @TipoIva, @Tipo, @FechaVenta, @VentaBruta,
                        @NumeroTransferencia, @Observacion, @Descuento, @VentaTot,
                        @Sincronizada, @Valida)";

            foreach (var proforma in dto.Proformas)
            {
                const string existsProforma = @"SELECT COUNT(1) FROM Proforma WHERE idProforma = @IdProforma";
                var yaExiste = await connection.ExecuteScalarAsync<int>(existsProforma, proforma, transaction);
                if (yaExiste > 0)
                {
                    continue;
                }
                await connection.ExecuteAsync(insertProforma, proforma, transaction);
            }

            const string insertProformaDetalle = @"
                INSERT INTO ProformaDetalle (idProformaDetalle, idProforma, codigoModelo,
                                             cantidad, precioVenta, fecha, valida)
                VALUES (@IdProformaDetalle, @IdProforma, @CodigoModelo,
                        @Cantidad, @PrecioVenta, @Fecha, @Valida)";

            foreach (var detalle in dto.ProformasDetalles)
            {
                const string existsProformaDetalle = @"SELECT COUNT(1) FROM ProformaDetalle WHERE idProformaDetalle = @IdProformaDetalle";
                var yaExiste = await connection.ExecuteScalarAsync<int>(existsProformaDetalle, detalle, transaction);
                if (yaExiste > 0)
                {
                    continue;
                }
                await connection.ExecuteAsync(insertProformaDetalle, detalle, transaction);
            }

            const string insertProformaDetalleGarantia = @"
                INSERT INTO ProformaDetalleGarantia (idProformaDetalle, idGarantia)
                VALUES (@IdProformaDetalle, @IdGarantia)";

            foreach (var garante in dto.ProformasDetallesGarantias)
            {
                const string existsProformaDetalleGarantia = @"SELECT COUNT(1) FROM ProformaDetalleGarantia WHERE idProformaDetalle = @IdProformaDetalle AND idGarantia = @IdGarantia";
                var yaExiste = await connection.ExecuteScalarAsync<int>(existsProformaDetalleGarantia, garante, transaction);
                if (yaExiste > 0)
                {
                    continue;
                }
                await connection.ExecuteAsync(insertProformaDetalleGarantia, garante, transaction);
            }

            var MaxNumProforma = dto.Proformas.Max(p => p.NumeroProforma) + 1;
            var codigoVendedor = dto.Proformas[0].CodigoVendedor;

            const string updateNumProforma = @"update VendedoresNumProforma set NumeroProforma = @NumeroProforma where codigoVendedor = @codigoVendedor";
            await connection.ExecuteAsync(updateNumProforma, new { NumeroProforma = MaxNumProforma, codigoVendedor = codigoVendedor }, transaction);

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}