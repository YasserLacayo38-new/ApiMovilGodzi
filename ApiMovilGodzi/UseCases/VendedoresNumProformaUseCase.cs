using ApiMovilGodzi.Models;
using ApiMovilGodzi.Repository;

namespace ApiMovilGodzi.UseCases;

public class VendedoresNumProformaUseCase
{
    private readonly VendedoresNumProformaRepository _repository;
    private readonly VendedoresIPRepository _vendedoresIPRepository;
    public VendedoresNumProformaUseCase(VendedoresNumProformaRepository repository, VendedoresIPRepository vendedoresIPRepository)
    {
        _repository = repository;
        _vendedoresIPRepository = vendedoresIPRepository;
    }

    public async Task<Result<IEnumerable<VendedoresNumProforma>>> GetAllNumProformasAsync()
    {
        try
        {
            var result = await _repository.GetAllAsync();
            return Result<IEnumerable<VendedoresNumProforma>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<VendedoresNumProforma>>.Failure(ex.Message);
        }
    }

    public async Task<Result<IEnumerable<VendedoresNumProforma>>> GetNumProformasByIpAsync(string ip)
    {
        try
        {
            var vendedorIp = await _vendedoresIPRepository.GetByIpAsync(ip);
            if (vendedorIp == null)
            {
                return Result<IEnumerable<VendedoresNumProforma>>.Failure("No existe un vendedor asociado al dispositivo.");
            }

            var result = await _repository.GetByVendedorAsync(vendedorIp.CodigoVendedor);
            if(result.Count() <= 0)
            {
                return Result<IEnumerable<VendedoresNumProforma>>.Failure("No existe un numero incial de proforma asociado al vendedor.");
            }
            return Result<IEnumerable<VendedoresNumProforma>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<VendedoresNumProforma>>.Failure(ex.Message);
        }
    }
}
