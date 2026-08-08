namespace ApiMovilGodzi.Models;

public class Venta
{
    public Guid IdVenta { get; set; }
    public string CodigoCliente { get; set; } = null!;
    public string CodigoVendedor { get; set; } = null!;
    public string CodigoListaPrecio { get; set; } = null!;
    public int TipoIva {  get; set; }
    public DateTime FechaVenta { get; set; }
    public decimal VentaBruta { get; set; }
    public decimal Descuento { get; set; }
    public decimal? VentaTot { get; set; }
    public bool Transferida { get; set; }
    public bool Valida { get; set; }
}
