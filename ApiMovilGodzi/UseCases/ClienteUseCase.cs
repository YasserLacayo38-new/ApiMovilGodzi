using ApiMovilGodzi.Models;
using ApiMovilGodzi.Repository;

namespace ApiMovilGodzi.UseCases;

public class ClienteUseCase
{
    private readonly ClienteRepository _repository;
    private readonly VendedoresDeviceRepository _vendedoresDeviceRepository;

    public ClienteUseCase(ClienteRepository repository, VendedoresDeviceRepository vendedoresDeviceRepository)
    {
        _repository = repository;
        _vendedoresDeviceRepository = vendedoresDeviceRepository;
    }

    public async Task<Result<IEnumerable<Cliente>>> GetAllClienteAsync()
    {
        try
        {
            var result = await _repository.GetAllAsync();
            return Result<IEnumerable<Cliente>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Cliente>>.Failure(ex.Message);
        }
    }

    public async Task<Result<IEnumerable<Cliente>>> GetClientesByDeviceAsync(string idDevice)
    {
        try
        {
            var vendedor = await _vendedoresDeviceRepository.GetByDeviceAsync(idDevice);
            if (vendedor == null)
            {
                return Result<IEnumerable<Cliente>>.Failure("No existe un vendedor asociado al dispositivo.");
            }

            var result = await _repository.GetAllAsyncByCodigoVendedor(vendedor.CodigoVendedor);
            return Result<IEnumerable<Cliente>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Cliente>>.Failure(ex.Message);
        }
    }
}
