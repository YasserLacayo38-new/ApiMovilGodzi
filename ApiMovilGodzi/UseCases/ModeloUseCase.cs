using ApiMovilGodzi.Models;
using ApiMovilGodzi.Repository;
using PruebaSuper_Aplication;

namespace ApiMovilGodzi.UseCases;

public class ModeloUseCase
{
    private readonly ModeloRepository _repository;
    public ModeloUseCase(ModeloRepository repository)
    {
        _repository = repository;
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
}
