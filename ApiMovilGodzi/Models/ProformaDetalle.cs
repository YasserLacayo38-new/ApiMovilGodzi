namespace ApiMovilGodzi.Models;

public class ProformaDetalle
{
    public int idProformaDetalle { get; set; }
    public Guid? IdProforma { get; set; }
    public string CodigoModelo { get; set; } = null!;
    public int Cantidad { get; set; }
    public decimal PrecioVenta { get; set; }
    public DateTime Fecha { get; set; }
    public bool Valida { get; set; }
}
