using ApiMovilGodzi.Models;
using ApiMovilGodzi.Repository;
using PruebaSuper_Aplication;

namespace ApiMovilGodzi.UseCases;

public class RemisionUseCase
{
    private readonly RemisionRepository _remisionRepository;
    private readonly VendedoresIPRepository _vendedoresIPRepository;

    public RemisionUseCase(RemisionRepository remisionRepository, VendedoresIPRepository vendedoresIPRepository)
    {
        _remisionRepository = remisionRepository;
        _vendedoresIPRepository = vendedoresIPRepository;
    }

    public async Task<Result<IEnumerable<Remision>>> GetAllRemisionAsync()
    {
        try
        {
            var result = await _remisionRepository.GetAllAsync();
            return Result<IEnumerable<Remision>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Remision>>.Failure(ex.Message);
        }
    }

    public async Task<Result<IEnumerable<Remision>>> GetAllRemisionByFechaAndIpAsync(DateTime fechaRemision, string ip)
    {
        try
        {
            var vendedor = await _vendedoresIPRepository.GetByIpAsync(ip);
            if (vendedor == null)
            {
                return Result<IEnumerable<Remision>>.Failure("No existe un vendedor asociado a la IP.");
            }

            var result = await _remisionRepository.GetAllAsyncByVendedorFechaRemision(vendedor.CodigoVendedor, fechaRemision);
            return Result<IEnumerable<Remision>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Remision>>.Failure(ex.Message);
        }
    }
}
