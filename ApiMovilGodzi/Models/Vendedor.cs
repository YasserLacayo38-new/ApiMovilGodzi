namespace ApiMovilGodzi.Models;

public class Vendedor
{
    public string CodigoVendedor { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public string Abreviatura { get; set; } = null!;
    public string? Telefono { get; set; }
}
