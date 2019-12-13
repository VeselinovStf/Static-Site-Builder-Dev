namespace Infrastructure.AdminSiteTypeWidgets.DTOs
{
    public class UsebleWidgetTypeDTO
    {
        public string WidgetId { get; set; }
        public string Name { get; set; }

        public string Functionality { get; set; }

        public decimal Price { get; set; }

        public int Version { get; set; }

        public bool IsOn { get; set; }

        public bool IsFree { get; set; }

        public string Implementation { get; set; }

        public bool IsAdded { get; set; }
    }
}