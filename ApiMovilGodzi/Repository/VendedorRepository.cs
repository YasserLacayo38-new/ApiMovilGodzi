using ApiMovilGodzi.Conexion;
using ApiMovilGodzi.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ApiMovilGodzi.Repository;

public class VendedorRepository
{
    private readonly ConnectionString _connectionString;
    public VendedorRepository(ConnectionString connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<Vendedor>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString.Connection);
        return await connection.QueryAsync<Vendedor>("SELECT * FROM Vendedor");
    }

    public async Task<Vendedor?> GetByIdAsync(string codigoVendedor)
    {
        using var connection = new SqlConnection(_connectionString.Connection);
        return await connection.QueryFirstOrDefaultAsync<Vendedor>(
            "SELECT * FROM Vendedor WHERE codigoVendedor = @CodigoVendedor",
            new { CodigoVendedor = codigoVendedor });
    }
}
