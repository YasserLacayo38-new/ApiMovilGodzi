namespace ApiMovilGodzi.Models.ModelsDto
{
    public class ProformaDetalleGarantiaDTO
    {
        public List<Proforma> Proformas { get; set; }
        public List<ProformaDetalle> ProformasDetalles {get; set ;}
        public List<ProformaDetalleGarantia> ProformasDetallesGarantias{ get; set;}
    }
}
