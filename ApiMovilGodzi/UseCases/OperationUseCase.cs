using ApiMovilGodzi.Models;
using ApiMovilGodzi.Repository;
using PruebaSuper_Aplication;

namespace ApiMovilGodzi.UseCases
{
    public class OperationUseCase
    {
        private readonly OperationRepository operationRepository;
        public OperationUseCase(OperationRepository operationRepository)
        {
            this.operationRepository = operationRepository;
        }

        public async Task<Result<ResultProcedure>> SyncListaPrecioAync()
        {
            try
            {
                var result = await operationRepository.SincronizarListaPrecioAsync();
                if(result.EstadoProcedure == -1)
                {
                    return Result<ResultProcedure>.Failure(result.Mensaje);
                }
                return Result<ResultProcedure>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<ResultProcedure>.Failure(ex.Message);
            }
        }

        public async Task<Result<ResultProcedure>> SyncClientesAsync(string ip)
        {
            try
            {
                var result = await operationRepository.SincronizarClientesAsync(ip);
                if (result.EstadoProcedure == -1)
                {
                    return Result<ResultProcedure>.Failure(result.Mensaje);
                }
                return Result<ResultProcedure>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<ResultProcedure>.Failure(ex.Message);
            }
        }

        public async Task<Result<ResultProcedure>> SyncVendedoresAsync()
        {
            try
            {
                var result = await operationRepository.SincronizarVendedoresAsync();
                if (result.EstadoProcedure == -1)
                {
                    return Result<ResultProcedure>.Failure(result.Mensaje);
                }
                return Result<ResultProcedure>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<ResultProcedure>.Failure(ex.Message);
            }
        }

        public async Task<Result<ResultProcedure>> SyncModelosAsync()
        {
            try
            {
                var result = await operationRepository.SincronizarModelosAsync();
                if (result.EstadoProcedure == -1)
                {
                    return Result<ResultProcedure>.Failure(result.Mensaje);
                }
                return Result<ResultProcedure>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<ResultProcedure>.Failure(ex.Message);
            }
        }

        public async Task<Result<ResultProcedure>> SyncRemisionesAsync(DateTime FechaRemision, string ip)
        {
            try
            {
                var result = await operationRepository.SincronizarRemisionesAsync(FechaRemision, ip);
                if (result.EstadoProcedure == -1)
                {
                    return Result<ResultProcedure>.Failure(result.Mensaje);
                }
                return Result<ResultProcedure>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<ResultProcedure>.Failure(ex.Message);
            }
        }
    }
}
