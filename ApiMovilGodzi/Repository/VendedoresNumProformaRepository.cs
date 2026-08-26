using ApiMovilGodzi.Conexion;
using ApiMovilGodzi.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ApiMovilGodzi.Repository;

public class VendedoresNumProformaRepository
{
    private readonly ConnectionString _connectionString;
    public VendedoresNumProformaRepository(ConnectionString connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<VendedoresNumProforma>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString.Connection);
        return await connection.QueryAsync<VendedoresNumProforma>("SELECT * FROM VendedoresNumProforma");
    }

    public async Task<IEnumerable<VendedoresNumProforma>> GetByVendedorAsync(string codigoVendedor)
    {
        using var connection = new SqlConnection(_connectionString.Connection);
        return await connection.QueryAsync<VendedoresNumProforma>(
            "SELECT * FROM VendedoresNumProforma WHERE codigoVendedor = @CodigoVendedor",
            new { CodigoVendedor = codigoVendedor });
    }
}
