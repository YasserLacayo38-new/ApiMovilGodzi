namespace ApiMovilGodzi.Models;

public class Proforma
{
    public Guid IdProforma { get; set; }
    public int NumeroProforma { get; set; }
    public string CodigoCliente { get; set; } = null!;
    public string CodigoVendedor { get; set; } = null!;
    public string CodigoListaPrecio { get; set; } = null!;
    public int TipoIva { get; set; }
    public int Tipo { get; set; }
    public DateTime FechaVenta { get; set; }
    public decimal VentaBruta { get; set; }
    public string? NumeroTransferencia { get; set; }
    public string? Observacion { get; set; }
    public decimal Descuento { get; set; }
    public decimal? VentaTot { get; set; }
    public bool Sincronizada { get; set; }
    public bool Valida { get; set; }
}
