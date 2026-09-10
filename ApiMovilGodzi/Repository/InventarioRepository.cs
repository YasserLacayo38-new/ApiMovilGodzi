using ApiMovilGodzi.Conexion;
using ApiMovilGodzi.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ApiMovilGodzi.Repository;

public class InventarioRepository
{
    private readonly ConnectionString _connectionString;
    public InventarioRepository(ConnectionString connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<Inventario>> GetAllByVendedorAsync(string codigoVendedor)
    {
        using var connection = new SqlConnection(_connectionString.Connection);
        return await connection.QueryAsync<Inventario>(
            "SELECT * FROM Inventario WHERE codigoVendedor = @CodigoVendedor",
            new { CodigoVendedor = codigoVendedor });
    }
}