using System.Collections.Generic;

namespace Infrastructure.SiteTypes.DTOs
{
    public class SiteTypeDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string BuildInName { get; set; }

        public decimal Price { get; set; }

        public IList<SiteTypeWidgetDTO> SiteTypeWidget { get; set; }
    }
}