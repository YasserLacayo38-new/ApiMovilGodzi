namespace ApiMovilGodzi.Models.ModelsDto
{
    public class RemisionDetalleDTO
    {
        public int IdRemision { get; set; }
        public string CodigoVendedor { get; set; } = null!;
        public DateTime FechaRemision { get; set; }
        public int IdRemisionDetalle { get; set; }
        public string Numcom { get; set; } = null!;
        public string CodigoModelo { get; set; } = null!;
        public int Cantidad { get; set; }
    }
}
