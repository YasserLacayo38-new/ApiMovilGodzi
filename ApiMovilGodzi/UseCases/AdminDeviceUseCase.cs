using ApiMovilGodzi.Models;
using ApiMovilGodzi.Repository;

namespace ApiMovilGodzi.UseCases;

public class AdminDeviceUseCase
{
    private readonly AdminDeviceRepository _repository;
    public AdminDeviceUseCase(AdminDeviceRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<AdminDevice>>> GetAllAdminDevicesAsync()
    {
        try
        {
            var result = await _repository.GetAllAsync();
            return Result<IEnumerable<AdminDevice>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<AdminDevice>>.Failure(ex.Message);
        }
    }
}