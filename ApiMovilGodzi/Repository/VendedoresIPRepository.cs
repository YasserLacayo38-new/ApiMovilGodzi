using ApiMovilGodzi.Conexion;
using ApiMovilGodzi.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ApiMovilGodzi.Repository;

public class VendedoresIPRepository
{
    private readonly ConnectionString _connectionString;
    public VendedoresIPRepository(ConnectionString connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<VendedoresIP>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString.Connection);
        return await connection.QueryAsync<VendedoresIP>("SELECT * FROM VendedoresIP");
    }

    public async Task<VendedoresIP?> GetByIpAsync(string ip)
    {
        using var connection = new SqlConnection(_connectionString.Connection);
        return await connection.QueryFirstOrDefaultAsync<VendedoresIP>(
            "SELECT * FROM VendedoresIP WHERE ipDispositivo = @Ip", new { Ip = ip });
    }
}
