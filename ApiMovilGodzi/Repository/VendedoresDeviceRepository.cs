using ApiMovilGodzi.Conexion;
using ApiMovilGodzi.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ApiMovilGodzi.Repository;

public class VendedoresDeviceRepository
{
    private readonly ConnectionString _connectionString;
    public VendedoresDeviceRepository(ConnectionString connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<VendedoresDevice>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString.Connection);
        return await connection.QueryAsync<VendedoresDevice>("SELECT * FROM VendedoresDevice");
    }

    public async Task<VendedoresDevice?> GetByDeviceAsync(string idDevice)
    {
        using var connection = new SqlConnection(_connectionString.Connection);
        return await connection.QueryFirstOrDefaultAsync<VendedoresDevice>(
            "SELECT * FROM VendedoresDevice WHERE idDevice = @IdDevice", new { IdDevice = idDevice });
    }
}