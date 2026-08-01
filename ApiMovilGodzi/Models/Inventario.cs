namespace ApiMovilGodzi.Models;

public class Inventario
{
    public int IdUnico { get; set; }
    public string CodigoVendedor { get; set; } = null!;
    public string CodigoModelo { get; set; } = null!;
    public int Cantidad { get; set; }
    public DateTime FechaRemision { get; set; }
}
