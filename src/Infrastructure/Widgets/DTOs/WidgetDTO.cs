namespace Infrastructure.Widgets.DTOs
{
    public class WidgetDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Functionality { get; set; }

        public string Implementation { get; set; }
        public decimal Price { get; set; }

        public int Version { get; set; }

        public double Votes { get; set; }

        public bool IsOn { get; set; }

        public bool IsFree { get; set; }

        public string SiteTypeSpecification { get; set; }       

        public string UsebleSiteType { get; set; }
      
        public string Dependency { get; set; }
    }
}