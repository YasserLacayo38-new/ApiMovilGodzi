using ApiMovilGodzi.Models;
using ApiMovilGodzi.Repository;

namespace ApiMovilGodzi.UseCases;

public class VendedorUseCase
{
    private readonly VendedorRepository _repository;
    private readonly VendedoresDeviceRepository _vendedoresDeviceRepository;
    public VendedorUseCase(VendedorRepository repository, VendedoresDeviceRepository vendedoresDeviceRepository)
    {
        _repository = repository;
        _vendedoresDeviceRepository = vendedoresDeviceRepository;
    }

    public async Task<Result<IEnumerable<Vendedor>>> GetAllVendedorAsync()
    {
        try
        {
            var result = await _repository.GetAllAsync();
            return Result<IEnumerable<Vendedor>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Vendedor>>.Failure(ex.Message);
        }
    }

    public async Task<Result<Vendedor>> GetVendedorByDeviceAsync(string idDevice)
    {
        try
        {
            var vendedorDevice = await _vendedoresDeviceRepository.GetByDeviceAsync(idDevice);
            if (vendedorDevice == null)
            {
                return Result<Vendedor>.Failure("No existe un vendedor asociado al dispositivo.");
            }

            var vendedor = await _repository.GetByIdAsync(vendedorDevice.CodigoVendedor);
            if (vendedor == null)
            {
                return Result<Vendedor>.Failure("No se encontró el vendedor.");
            }

            return Result<Vendedor>.Success(vendedor);
        }
        catch (Exception ex)
        {
            return Result<Vendedor>.Failure(ex.Message);
        }
    }
}
