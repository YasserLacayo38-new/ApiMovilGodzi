using ApiMovilGodzi.Conexion;
using ApiMovilGodzi.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ApiMovilGodzi.Repository;

public class GarantiaRepository
{
    private readonly ConnectionString _connectionString;
    public GarantiaRepository(ConnectionString connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<Garantia>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString.Connection);
        return await connection.QueryAsync<Garantia>("SELECT * FROM Garantia");
    }

    public async Task<IEnumerable<Garantia>> GetByVendedorAndFechaAsync(string codigoVendedor, DateTime fechaRemision)
    {
        const string sql = @"
            SELECT * FROM Garantia
            WHERE codigoVendedor = @CodigoVendedor
              AND CAST(fechaRemision AS DATE) = CAST(@FechaRemision AS DATE)";

        using var connection = new SqlConnection(_connectionString.Connection);
        return await connection.QueryAsync<Garantia>(sql,
            new { CodigoVendedor = codigoVendedor, FechaRemision = fechaRemision });
    }
}
