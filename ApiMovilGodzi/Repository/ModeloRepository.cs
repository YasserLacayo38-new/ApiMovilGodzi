using ApiMovilGodzi.Conexion;
using ApiMovilGodzi.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ApiMovilGodzi.Repository;

public class ModeloRepository
{
    private readonly ConnectionString _connectionString;
    public ModeloRepository(ConnectionString connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<Modelo>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString.Connection);
        return await connection.QueryAsync<Modelo>("SELECT * FROM Modelo");
    }

    public async Task<IEnumerable<Modelo>> GetModelosByVendedorFechaRemision(
        string codigoVendedor, DateTime fechaRemision)
    {
        const string sql = @"
            SELECT DISTINCT m.codigoModelo, m.codigo_vta AS CodigoVta, m.descripcion, m.precioVenta
            FROM Modelo m
            INNER JOIN RemisionDetalle rd ON m.codigoModelo = rd.codigoModelo
            INNER JOIN Remision r ON r.numcom = rd.numcom
            WHERE r.codigoVendedor = @CodigoVendedor
              AND CAST(r.fechaRemision AS DATE) = CAST(@FechaRemision AS DATE)";

        using var connection = new SqlConnection(_connectionString.Connection);
        return await connection.QueryAsync<Modelo>(sql,
            new { CodigoVendedor = codigoVendedor, FechaRemision = fechaRemision });
    }
}
