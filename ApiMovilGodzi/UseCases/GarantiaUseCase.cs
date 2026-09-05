using ApiMovilGodzi.Models;
using ApiMovilGodzi.Repository;

namespace ApiMovilGodzi.UseCases;

public class GarantiaUseCase
{
    private readonly GarantiaRepository _repository;
    private readonly VendedoresIPRepository _vendedoresIPRepository;
    public GarantiaUseCase(GarantiaRepository repository, VendedoresIPRepository vendedoresIPRepository)
    {
        _repository = repository;
        _vendedoresIPRepository = vendedoresIPRepository;
    }

    public async Task<Result<IEnumerable<Garantia>>> GetAllGarantiasAsync()
    {
        try
        {
            var result = await _repository.GetAllAsync();
            return Result<IEnumerable<Garantia>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Garantia>>.Failure(ex.Message);
        }
    }

    public async Task<Result<IEnumerable<Garantia>>> GetGarantiasByIpAndFechaAsync(string ip, DateTime fechaRemision)
    {
        try
        {
            var vendedorIp = await _vendedoresIPRepository.GetByIpAsync(ip);
            if (vendedorIp == null)
            {
                return Result<IEnumerable<Garantia>>.Failure("No existe un vendedor asociado al dispositivo.");
            }

            var result = await _repository.GetByVendedorAndFechaAsync(vendedorIp.CodigoVendedor, fechaRemision);
            return Result<IEnumerable<Garantia>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Garantia>>.Failure(ex.Message);
        }
    }
}
