namespace ApplicationCore.Entities.WidjetsEntity
{
    public class ClientWidjet
    {
        public string ClientId { get; set; }

        public string WidjetId { get; set; }

        public WidjetElement Widjet { get; set; }
    }
}