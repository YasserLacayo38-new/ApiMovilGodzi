using ApiMovilGodzi.Conexion;
using ApiMovilGodzi.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ApiMovilGodzi.Repository;

public class ClienteRepository
{
    private readonly ConnectionString _connectionString;
    public ClienteRepository(ConnectionString connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString.Connection);
        return await connection.QueryAsync<Cliente>("SELECT * FROM Cliente");
    }

    public async Task<IEnumerable<Cliente>> GetAllAsyncByCodigoVendedor(string codigoVendedor)
    {
        using var connection = new SqlConnection(_connectionString.Connection);
        return await connection.QueryAsync<Cliente>(
            "SELECT * FROM Cliente WHERE codigoVendedor = @CodigoVendedor",
            new { CodigoVendedor = codigoVendedor });
    }
}
