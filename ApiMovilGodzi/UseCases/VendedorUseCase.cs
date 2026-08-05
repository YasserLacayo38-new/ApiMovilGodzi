using ApiMovilGodzi.Models;
using ApiMovilGodzi.Repository;
using PruebaSuper_Aplication;

namespace ApiMovilGodzi.UseCases;

public class VendedorUseCase
{
    private readonly VendedorRepository _repository;
    private readonly VendedoresIPRepository _vendedoresIPRepository;
    public VendedorUseCase(VendedorRepository repository, VendedoresIPRepository vendedoresIPRepository)
    {
        _repository = repository;
        _vendedoresIPRepository = vendedoresIPRepository;
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

    public async Task<Result<Vendedor>> GetVendedorByIpAsync(string ip)
    {
        try
        {
            var vendedorIp = await _vendedoresIPRepository.GetByIpAsync(ip);
            if (vendedorIp == null)
            {
                return Result<Vendedor>.Failure("No existe un vendedor asociado a la IP.");
            }

            var vendedor = await _repository.GetByIdAsync(vendedorIp.CodigoVendedor);
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
