using ApiMovilGodzi.Models;
using ApiMovilGodzi.Repository;

namespace ApiMovilGodzi.UseCases;

public class LineaUseCase
{
    private readonly LineaRepository _repository;
    public LineaUseCase(LineaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<Linea>>> GetAllLineasAsync()
    {
        try
        {
            var result = await _repository.GetAllAsync();
            return Result<IEnumerable<Linea>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Linea>>.Failure(ex.Message);
        }
    }
}