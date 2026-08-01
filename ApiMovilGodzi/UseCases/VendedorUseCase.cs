using ApiMovilGodzi.Models;
using ApiMovilGodzi.Repository;
using PruebaSuper_Aplication;

namespace ApiMovilGodzi.UseCases;

public class VendedorUseCase
{
    private readonly VendedorRepository _repository;
    public VendedorUseCase(VendedorRepository repository)
    {
        _repository = repository;
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
}
