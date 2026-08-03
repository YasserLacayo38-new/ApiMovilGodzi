namespace ApiMovilGodzi.Models;

public class Remision
{
    public int IdRemision { get; set; }
    public string Numcom { get; set; } = null!;
    public string CodigoVendedor { get; set; } = null!;
    public DateTime FechaRemision { get; set; }
    public List<RemisionDetalle> RemisionDetalles { get; set; } = new();
}
