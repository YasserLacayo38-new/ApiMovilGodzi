namespace ApiMovilGodzi.Models;

public class RemisionDetalle
{
    public int IdRemisionDetalle { get; set; }
    public string Numcom { get; set; } = null!;
    public string CodigoModelo { get; set; } = null!;
    public int Cantidad { get; set; }
}
