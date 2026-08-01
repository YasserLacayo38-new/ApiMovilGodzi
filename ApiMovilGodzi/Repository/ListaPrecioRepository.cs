using ApiMovilGodzi.Conexion;
using ApiMovilGodzi.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ApiMovilGodzi.Repository;

public class ListaPrecioRepository
{
    private readonly ConnectionString _connectionString;
    public ListaPrecioRepository(ConnectionString connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<ListaPrecio>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString.Connection);
        return await connection.QueryAsync<ListaPrecio>("SELECT * FROM ListaPrecio");
    }
}
