using ApiMovilGodzi.Models;
using ApiMovilGodzi.Repository;

namespace ApiMovilGodzi.UseCases;

public class InventarioUseCase
{
    private readonly InventarioRepository _repository;
    private readonly VendedoresIPRepository _vendedoresIPRepository;

    public InventarioUseCase(InventarioRepository repository, VendedoresIPRepository vendedoresIPRepository)
    {
        _repository = repository;
        _vendedoresIPRepository = vendedoresIPRepository;
    }

    public async Task<Result<IEnumerable<Inventario>>> GetInventarioByIpAsync(string ip)
    {
        try
        {
            var vendedorIp = await _vendedoresIPRepository.GetByIpAsync(ip);
            if (vendedorIp == null)
            {
                return Result<IEnumerable<Inventario>>.Failure("No existe un vendedor asociado al dispositivo.");
            }

            var result = await _repository.GetAllByVendedorAsync(vendedorIp.CodigoVendedor);
            return Result<IEnumerable<Inventario>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Inventario>>.Failure(ex.Message);
        }
    }
}