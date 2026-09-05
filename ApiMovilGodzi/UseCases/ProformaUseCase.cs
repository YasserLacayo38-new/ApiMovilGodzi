using ApiMovilGodzi.Models.ModelsDto;
using ApiMovilGodzi.Repository;

namespace ApiMovilGodzi.UseCases;

public class ProformaUseCase
{
    private readonly ProformaRepository _repository;
    public ProformaUseCase(ProformaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<bool>> AddProformasAsync(ProformaDetalleGarantiaDTO dto)
    {
        try
        {
            await _repository.AddProformasListAsync(dto);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(ex.Message);
        }
    }
}