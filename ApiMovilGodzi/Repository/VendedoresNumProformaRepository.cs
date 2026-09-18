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
        const string sql = @"SELECT vnp.idVendedoresNumProforma,
                                    vnp.numeroProforma,
                                    vnp.codigoVendedor,
                                    v.nombre AS Vendedor
                             FROM VendedoresNumProforma vnp
                             INNER JOIN Vendedor v ON v.codigoVendedor = vnp.codigoVendedor";
        return await connection.QueryAsync<VendedoresNumProforma>(sql);
    }

    public async Task<bool> ExistsByCodigoVendedorAsync(string codigoVendedor)
    {
        using var connection = new SqlConnection(_connectionString.Connection);
        return await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(1) FROM VendedoresNumProforma WHERE codigoVendedor = @CodigoVendedor",
            new { CodigoVendedor = codigoVendedor }) > 0;
    }

    public async Task AddAsync(VendedoresNumProforma vendedoresNumProforma)
    {
        using var connection = new SqlConnection(_connectionString.Connection);
        await connection.ExecuteAsync(
            "INSERT INTO VendedoresNumProforma (numeroProforma, codigoVendedor) VALUES (@NumeroProforma, @CodigoVendedor)",
            new { vendedoresNumProforma.NumeroProforma, vendedoresNumProforma.CodigoVendedor });
    }

    public async Task<IEnumerable<VendedoresNumProforma>> GetByVendedorAsync(string codigoVendedor)
    {
        using var connection = new SqlConnection(_connectionString.Connection);
        return await connection.QueryAsync<VendedoresNumProforma>(
            "SELECT * FROM VendedoresNumProforma WHERE codigoVendedor = @CodigoVendedor",
            new { CodigoVendedor = codigoVendedor });
    }
}
