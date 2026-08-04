using ApiMovilGodzi.Models;
using ApiMovilGodzi.Repository;
using PruebaSuper_Aplication;

namespace ApiMovilGodzi.UseCases;

public class ClienteUseCase
{
    private readonly ClienteRepository _repository;
    private readonly VendedoresIPRepository _vendedoresIPRepository;

    public ClienteUseCase(ClienteRepository repository, VendedoresIPRepository vendedoresIPRepository)
    {
        _repository = repository;
        _vendedoresIPRepository = vendedoresIPRepository;
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

    public async Task<Result<IEnumerable<Cliente>>> GetClientesByIpAsync(string ip)
    {
        try
        {
            var vendedor = await _vendedoresIPRepository.GetByIpAsync(ip);
            if (vendedor == null)
            {
                return Result<IEnumerable<Cliente>>.Failure("No existe un vendedor asociado a la IP.");
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
