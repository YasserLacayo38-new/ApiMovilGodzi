using ApiMovilGodzi.Models;
using ApiMovilGodzi.Repository;

namespace ApiMovilGodzi.UseCases;

public class VendedoresNumProformaUseCase
{
    private readonly VendedoresNumProformaRepository _repository;
    private readonly VendedoresDeviceRepository _vendedoresDeviceRepository;
    private readonly VendedorRepository _venendedorRepository;
public VendedoresNumProformaUseCase(VendedoresNumProformaRepository repository, VendedoresDeviceRepository vendedoresDeviceRepository, VendedorRepository venendedorRepository)
    {
        _repository = repository;
        _vendedoresDeviceRepository = vendedoresDeviceRepository;
        _venendedorRepository = venendedorRepository;
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

public async Task<Result<IEnumerable<VendedoresNumProforma>>> GetNumProformasByDeviceAsync(string idDevice)
    {
        try
        {
            var vendedorDevice = await _vendedoresDeviceRepository.GetByDeviceAsync(idDevice);
            if (vendedorDevice == null)
            {
                return Result<IEnumerable<VendedoresNumProforma>>.Failure("No existe un vendedor asociado al dispositivo.");
            }

            var result = await _repository.GetByVendedorAsync(vendedorDevice.CodigoVendedor);
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

    public async Task<Result<bool>> AddNumProformaAsync(int numeroProforma, string codigoVendedor)
    {
        if (numeroProforma <= 0)
        {
            return Result<bool>.Failure("El número de proforma no puede ser cero o menor a cero.");
        }

        if (await _repository.ExistsByCodigoVendedorAsync(codigoVendedor))
        {
            return Result<bool>.Failure("El vendedor ya tiene un número de proforma registrado.");
        }

        var Vendedor = await _venendedorRepository.GetByIdAsync(codigoVendedor);

        if (Vendedor == null)
        {
            return Result<bool>.Failure("El vendedor no existe.");
        }

        await _repository.AddAsync(new VendedoresNumProforma
        {
            NumeroProforma = numeroProforma,
            CodigoVendedor = codigoVendedor
        });
        return Result<bool>.Success(true);
    }
}
