using ApiMovilGodzi.Models;
using ApiMovilGodzi.Repository;
using PruebaSuper_Aplication;

namespace ApiMovilGodzi.UseCases;

public class ModeloUseCase
{
    private readonly ModeloRepository _repository;
    private readonly VendedoresIPRepository _vendedoresIPRepository;

    public ModeloUseCase(ModeloRepository repository, VendedoresIPRepository vendedoresIPRepository)
    {
        _repository = repository;
        _vendedoresIPRepository = vendedoresIPRepository;
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

    public async Task<Result<IEnumerable<Modelo>>> GetModelosByIpAndFechaRemisionAsync(string ip, DateTime fechaRemision)
    {
        try
        {
            var vendedor = await _vendedoresIPRepository.GetByIpAsync(ip);
            if (vendedor == null)
            {
                return Result<IEnumerable<Modelo>>.Failure("No existe un vendedor asociado a la IP.");
            }

            var result = await _repository.GetModelosByVendedorFechaRemision(vendedor.CodigoVendedor, fechaRemision);
            return Result<IEnumerable<Modelo>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Modelo>>.Failure(ex.Message);
        }
    }
}
