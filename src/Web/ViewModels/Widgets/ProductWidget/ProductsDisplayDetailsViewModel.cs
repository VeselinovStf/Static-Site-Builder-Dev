using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels.Widgets.ProductWidget
{
    public class ProductsDisplayDetailsViewModel
    {
        public string ProjectId { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public decimal DiscountProcent { get; set; }
        public DateTime DiscountTimer { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string DetailsLink { get; set; }
        public string Details { get; set; }
        public bool State { get; set; }

        public string Color { get; set; }
        public string Size { get; set; }
        public string Gender { get; set; }
        public string Collection { get; set; }
        public bool Hot { get; set; }
        public bool LatestProduct { get; set; }
        public bool LatestNewCollectionProduct { get; set; }
        public bool DealOfTheDayProduct { get; set; }
        public bool ProductOfTheDay { get; set; }
        public bool PickedForYou { get; set; }
        public string Category { get; set; }
        public double Rating { get; set; }

        public string Image { get; set; }



    }
}
