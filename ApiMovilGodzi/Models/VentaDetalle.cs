namespace ApiMovilGodzi.Models;

public class VentaDetalle
{
    public int IdVentaDetalle { get; set; }
    public Guid? IdVenta { get; set; }
    public string CodigoModelo { get; set; } = null!;
    public int Cantidad { get; set; }
    public decimal Bonificacion { get; set; }
    public decimal PrecioVenta { get; set; }
    public DateTime Fecha { get; set; }
    public bool Valida { get; set; }
}
