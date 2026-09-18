using ApiMovilGodzi.Models;
using ApiMovilGodzi.Repository;

namespace ApiMovilGodzi.UseCases;

public class RemisionUseCase
{
    private readonly RemisionRepository _remisionRepository;
    private readonly VendedoresDeviceRepository _vendedoresDeviceRepository;

    public RemisionUseCase(RemisionRepository remisionRepository, VendedoresDeviceRepository vendedoresDeviceRepository)
    {
        _remisionRepository = remisionRepository;
        _vendedoresDeviceRepository = vendedoresDeviceRepository;
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

    public async Task<Result<IEnumerable<Remision>>> GetAllRemisionByFechaAndDeviceAsync(DateTime fechaRemision, string idDevice)
    {
        try
        {
            var vendedor = await _vendedoresDeviceRepository.GetByDeviceAsync(idDevice);
            if (vendedor == null)
            {
                return Result<IEnumerable<Remision>>.Failure("No existe un vendedor asociado al dispositivo.");
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
