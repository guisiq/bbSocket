namespace ApiUnitins.Model
{
    public class ReceberPix
    {
        public List<Pix> pix { get; set; }
    }

    public class ComponentesValor
    {
        public Original original { get; set; }
    }

    public class Original
    {
        public string valor { get; set; }
    }

    public class Pagador
    {
        public string cpf { get; set; }
        public string nome { get; set; }
    }

    public class Pix
    {
        public string endToEndId { get; set; }
        public string txid { get; set; }
        public string valor { get; set; }
        public ComponentesValor componentesValor { get; set; }
        public string chave { get; set; }
        public DateTime horario { get; set; }
        public string infoPagador { get; set; }
        public Pagador pagador { get; set; }
    }

}
