using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Interfaces;
using System;

namespace ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate
{
    public class Product : DescriptiveEntity, IProductWidjets
    {
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public decimal DiscountProcent { get; set; }
        public DateTime DiscountTimer { get; set; }
        public int TemplateId { get; set; }

        public string Brand { get; set; }
        public string DetailsLink { get; set; }
        public string Details { get; set; }
        public bool State { get; set; }
        public string Link { get; set; }
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

        public bool Publish { get; set; }

        public string Image { get; set; }

        public double Rating { get; set; }

        /// <summary>
        /// Relation to Project
        /// </summary>
        public string ProjectId { get; set; }

        public StoreTypeSite Project { get; set; }

        public ProductFrontMatter FrontMatter { get; set; }
    }
}