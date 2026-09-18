using ApiMovilGodzi.Models;
using ApiMovilGodzi.Repository;

namespace ApiMovilGodzi.UseCases;

public class ModeloUseCase
{
    private readonly ModeloRepository _repository;
    private readonly VendedoresDeviceRepository _vendedoresDeviceRepository;

    public ModeloUseCase(ModeloRepository repository, VendedoresDeviceRepository vendedoresDeviceRepository)
    {
        _repository = repository;
        _vendedoresDeviceRepository = vendedoresDeviceRepository;
    }

    public async Task<Result<IEnumerable<Modelo>>> GetAllModeloAsync()
    {
        try
        {
            var result = await _repository.GetAllAsync();
            return Result<IEnumerable<Modelo>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Modelo>>.Failure(ex.Message);
        }
    }

    public async Task<Result<IEnumerable<Modelo>>> GetModelosByDeviceAndFechaRemisionAsync(string idDevice, DateTime fechaRemision)
    {
        try
        {
            var vendedor = await _vendedoresDeviceRepository.GetByDeviceAsync(idDevice);
            if (vendedor == null)
            {
                return Result<IEnumerable<Modelo>>.Failure("No existe un vendedor asociado al dispositivo.");
            }

            var result = await _repository.GetModelosByVendedorFechaRemision(vendedor.CodigoVendedor, fechaRemision);
            return Result<IEnumerable<Modelo>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Modelo>>.Failure(ex.Message);
        }
    }

    public async Task<Result<IEnumerable<Modelo>>> GetModelosByDevice(string idDevice)
    {
        try
        {
            var vendedor = await _vendedoresDeviceRepository.GetByDeviceAsync(idDevice);
            if (vendedor == null)
            {
                return Result<IEnumerable<Modelo>>.Failure("No existe un vendedor asociado al dispositivo.");
            }

            var result = await _repository.GetModelosByVendedorInventario(vendedor.CodigoVendedor);
            return Result<IEnumerable<Modelo>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Modelo>>.Failure(ex.Message);
        }
    }
}
