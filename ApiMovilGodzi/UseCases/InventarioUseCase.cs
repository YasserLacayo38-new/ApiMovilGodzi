using ApiMovilGodzi.Models;
using ApiMovilGodzi.Repository;

namespace ApiMovilGodzi.UseCases;

public class InventarioUseCase
{
    private readonly InventarioRepository _repository;
    private readonly VendedoresDeviceRepository _vendedoresDeviceRepository;

    public InventarioUseCase(InventarioRepository repository, VendedoresDeviceRepository vendedoresDeviceRepository)
    {
        _repository = repository;
        _vendedoresDeviceRepository = vendedoresDeviceRepository;
    }

    public async Task<Result<IEnumerable<Inventario>>> GetInventarioByDeviceAsync(string idDevice)
    {
        try
        {
            var vendedorDevice = await _vendedoresDeviceRepository.GetByDeviceAsync(idDevice);
            if (vendedorDevice == null)
            {
                return Result<IEnumerable<Inventario>>.Failure("No existe un vendedor asociado al dispositivo.");
            }

            var result = await _repository.GetAllByVendedorAsync(vendedorDevice.CodigoVendedor);
            return Result<IEnumerable<Inventario>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Inventario>>.Failure(ex.Message);
        }
    }
}