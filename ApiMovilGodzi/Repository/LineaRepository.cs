using ApiMovilGodzi.Conexion;
using ApiMovilGodzi.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ApiMovilGodzi.Repository;

public class LineaRepository
{
    private readonly ConnectionString _connectionString;
    public LineaRepository(ConnectionString connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<Linea>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString.Connection);
        return await connection.QueryAsync<Linea>("SELECT * FROM Linea");
    }
}