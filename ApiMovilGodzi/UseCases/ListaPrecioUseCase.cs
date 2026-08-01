using ApiMovilGodzi.Models;
using ApiMovilGodzi.Repository;
using PruebaSuper_Aplication;

namespace ApiMovilGodzi.UseCases;

public class ListaPrecioUseCase
{
    private readonly ListaPrecioRepository _repository;
    public ListaPrecioUseCase(ListaPrecioRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<ListaPrecio>>> GetAllListaPrecioAsync()
    {
        try
        {
            var result = await _repository.GetAllAsync();
            return Result<IEnumerable<ListaPrecio>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<ListaPrecio>>.Failure(ex.Message);
        }
    }
}
