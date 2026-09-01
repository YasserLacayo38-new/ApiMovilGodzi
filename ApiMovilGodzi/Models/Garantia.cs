namespace ApiMovilGodzi.Models;

public class Garantia
{
    public int IdGarantia { get; set; }
    public string CodigoGarantia { get; set; } = null!;
    public string CodigoModelo { get; set; } = null!;
    public string CodigoVendedor { get; set; } = null!;
    public DateTime FechaRemision { get; set; }
}
