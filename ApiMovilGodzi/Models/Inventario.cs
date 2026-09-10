namespace ApiMovilGodzi.Models;

public class Inventario
{
    public int IdInventario { get; set; }
    public string CodigoModelo { get; set; } = null!;
    public string CodigoVendedor { get; set; } = null!;
    public int CantidadActual { get; set; }
    public int CantidadInicial { get; set; }
}