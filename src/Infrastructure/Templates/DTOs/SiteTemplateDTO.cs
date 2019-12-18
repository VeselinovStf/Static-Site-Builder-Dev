namespace Infrastructure.Templates.DTOs
{
    public class SiteTemplateDTO
    {
        public string ClientId { get; set; }
        public string Name { get; set; }

        public string SiteType { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}