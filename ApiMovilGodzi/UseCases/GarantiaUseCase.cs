using ApiMovilGodzi.Models;
using ApiMovilGodzi.Repository;

namespace ApiMovilGodzi.UseCases;

public class GarantiaUseCase
{
    private readonly GarantiaRepository _repository;
    private readonly VendedoresDeviceRepository _vendedoresDeviceRepository;
    public GarantiaUseCase(GarantiaRepository repository, VendedoresDeviceRepository vendedoresDeviceRepository)
    {
        _repository = repository;
        _vendedoresDeviceRepository = vendedoresDeviceRepository;
    }

    public async Task<Result<IEnumerable<Garantia>>> GetAllGarantiasAsync()
    {
        try
        {
            var result = await _repository.GetAllAsync();
            return Result<IEnumerable<Garantia>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Garantia>>.Failure(ex.Message);
        }
    }

    public async Task<Result<IEnumerable<Garantia>>> GetGarantiasByDeviceAndFechaAsync(string idDevice, DateTime fechaRemision)
    {
        try
        {
            var vendedorDevice = await _vendedoresDeviceRepository.GetByDeviceAsync(idDevice);
            if (vendedorDevice == null)
            {
                return Result<IEnumerable<Garantia>>.Failure("No existe un vendedor asociado al dispositivo.");
            }

            var result = await _repository.GetByVendedorAndFechaAsync(vendedorDevice.CodigoVendedor, fechaRemision);
            return Result<IEnumerable<Garantia>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Garantia>>.Failure(ex.Message);
        }
    }

    public async Task<Result<IEnumerable<Garantia>>> GetGarantiasDisponiblesByDeviceAsync(string idDevice)
    {
        try
        {
            var vendedorDevice = await _vendedoresDeviceRepository.GetByDeviceAsync(idDevice);
            if (vendedorDevice == null)
            {
                return Result<IEnumerable<Garantia>>.Failure("No existe un vendedor asociado al dispositivo.");
            }

            var result = await _repository.GetAllGarantiasDisponibleByVendedor(vendedorDevice.CodigoVendedor);
            return Result<IEnumerable<Garantia>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Garantia>>.Failure(ex.Message);
        }
    }
}
