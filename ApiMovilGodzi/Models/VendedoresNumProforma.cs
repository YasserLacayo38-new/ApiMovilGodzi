namespace ApiMovilGodzi.Models;

public class VendedoresNumProforma
{
    public int IdVendedoresNumProforma { get; set; }
    public int NumeroProforma { get; set; }
    public string CodigoVendedor { get; set; } = null!;
    public string Vendedor { get; set; } = null!;
}
