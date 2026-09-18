using ApiMovilGodzi.Conexion;
using ApiMovilGodzi.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiMovilGodzi.Repository
{
    public class OperationRepository
    {
        private readonly ConnectionString _connectionString;
        public OperationRepository(ConnectionString connectionString) 
        {
            this._connectionString = connectionString;
        }

        public async Task<ResultProcedure> SincronizarListaPrecioAsync()
        {
            ResultProcedure result;
            using (var connection = new SqlConnection(_connectionString.Connection))
            {
                 result = await connection.QueryFirstAsync<ResultProcedure>("spGetListaPrecioFromGodziDatabase",new {}, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public async Task<ResultProcedure> SincronizarLineasAsync()
        {
            using (var connection = new SqlConnection(_connectionString.Connection))
            {
                return await connection.QueryFirstAsync<ResultProcedure>(
                    "spGetLineasFromGodziDatabase",
                    new { },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<ResultProcedure> SincronizarClientesAsync(string IdDevice)
        {
            using (var connection = new SqlConnection(_connectionString.Connection))
            {
                return await connection.QueryFirstAsync<ResultProcedure>(
                    "spGetClientesFromGodziDatabase",
                    new { IdDevice = IdDevice },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<ResultProcedure> SincronizarVendedoresAsync()
        {
            using (var connection = new SqlConnection(_connectionString.Connection))
            {
                return await connection.QueryFirstAsync<ResultProcedure>(
                    "spGetVendedoresFromGodziDatabase",
                    new { },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<ResultProcedure> SincronizarModelosAsync()
        {
            using (var connection = new SqlConnection(_connectionString.Connection))
            {
                return await connection.QueryFirstAsync<ResultProcedure>(
                    "spGetModelosFromGodziDatabase",
                    new { },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<ResultProcedure> SincronizarRemisionesAsync(DateTime date, string IdDevice)
        {
            using (var connection = new SqlConnection(_connectionString.Connection))
            {
                return await connection.QueryFirstAsync<ResultProcedure>(
                    "spGetRemisionesFromGodziDatabase",
                    new {
                        FechaRemision = date,
                        IdDevice = IdDevice
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
