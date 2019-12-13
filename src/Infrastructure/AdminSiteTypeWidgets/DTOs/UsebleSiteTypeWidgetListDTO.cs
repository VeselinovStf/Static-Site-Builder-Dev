using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.AdminSiteTypeWidgets.DTOs
{
    public class UsebleSiteTypeWidgetListDTO
    {
        public string SiteTypeId { get; set; }

        public string SiteTypeName { get; set; }
        public IList<UsebleWidgetTypeDTO> UsebleWidgets { get; set; }
    }
}
