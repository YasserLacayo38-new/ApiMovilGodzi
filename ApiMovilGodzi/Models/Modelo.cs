namespace ApiMovilGodzi.Models;

public class Modelo
{
    public string CodigoModelo { get; set; } = null!;
    public string CodigoVta { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public decimal? PrecioVenta { get; set; }
}
