using ApiMovilGodzi.Conexion;
using ApiMovilGodzi.Models;
using ApiMovilGodzi.Models.ModelsDto;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ApiMovilGodzi.Repository;

public class RemisionRepository
{
    private readonly ConnectionString _connectionString;
    public RemisionRepository(ConnectionString connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<Remision>> GetAllAsync()
    {
        const string sql = @"
            SELECT r.idRemision, r.numcom, r.codigoVendedor, r.fechaRemision,
                   rd.idRemisionDetalle, rd.codigoModelo, rd.cantidad
            FROM Remision r
            INNER JOIN RemisionDetalle rd ON r.numcom = rd.numcom";

        using var connection = new SqlConnection(_connectionString.Connection);
        var dtos = await connection.QueryAsync<RemisionDetalleDTO>(sql);

        return MapToRemision(dtos);
    }

    public async Task<IEnumerable<Remision>> GetAllAsyncByVendedorFechaRemision(
        string codigoVendedor, DateTime fechaRemision)
    {
        const string sql = @"
            SELECT r.idRemision, r.numcom, r.codigoVendedor, r.fechaRemision,
                   rd.idRemisionDetalle, rd.codigoModelo, rd.cantidad
            FROM Remision r
            INNER JOIN RemisionDetalle rd ON r.numcom = rd.numcom
            WHERE r.codigoVendedor = @CodigoVendedor
              AND CAST(r.fechaRemision AS DATE) = CAST(@FechaRemision AS DATE)";

        using var connection = new SqlConnection(_connectionString.Connection);
        var dtos = await connection.QueryAsync<RemisionDetalleDTO>(sql,
            new { CodigoVendedor = codigoVendedor, FechaRemision = fechaRemision });

        return MapToRemision(dtos);
    }

    private static IEnumerable<Remision> MapToRemision(IEnumerable<RemisionDetalleDTO> dtos) =>
        dtos.GroupBy(d => d.IdRemision).Select(g =>
        {
            var first = g.First();
            return new Remision
            {
                IdRemision = first.IdRemision,
                Numcom = first.Numcom,
                CodigoVendedor = first.CodigoVendedor,
                FechaRemision = first.FechaRemision,
                RemisionDetalles = g.Select(d => new RemisionDetalle
                {
                    IdRemisionDetalle = d.IdRemisionDetalle,
                    Numcom = d.Numcom,
                    CodigoModelo = d.CodigoModelo,
                    Cantidad = d.Cantidad
                }).ToList()
            };
        });
}
