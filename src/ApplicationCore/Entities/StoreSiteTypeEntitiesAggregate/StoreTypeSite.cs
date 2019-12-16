using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Entities.SiteType;
using ApplicationCore.Entities.WidjetsEntityAggregate;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;

namespace ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate
{
    public class StoreTypeSite : DescriptiveEntity, IAggregateRoot, ISelingEntity
    {
        public string TemplateName { get; set; }
        public string ClientId { get; set; }
        public string LaunchingConfigId { get; set; }
        public LaunchConfig LaunchingConfig { get; set; }

        public bool IsFree { get; set; }

        public decimal Price
        {
            get
            {
                if (this.IsFree)
                {
                    return 0m;
                }
                else
                {
                    return this.Price;
                }
            }
            set
            {
                this.Price = value;
            }
        }
        public string ProjectId { get; set; }
        public ICollection<SiteWidget> SiteUsedWidgets { get; set; }

        private readonly List<Product> _products = new List<Product>();

        public IReadOnlyCollection<Product> Products
        {
            get
            {
                return new List<Product>(_products.AsReadOnly());
            }
        }

        //Build in widjets
       

        



        //Build in site type config
        public SiteTypesEnum SiteTypeSpecification
        {
            get
            {
                return SiteTypesEnum.StoreType;
            }
        }

       

      

        public void AddProduct(string projectId, string name, string description,
            bool hot, bool latestNewCollectionProduct, bool latestProduct,
            bool pickedForYou, decimal price, string size, bool state,
            string gender, int amount, string brand, string category,
            string collection, string color, bool dealOfTheDayProduct,
            string details, string detailsLink, decimal discountProcent,
            DateTime discountTimer, bool productOfTheDay,
            double rating, string image, IEnumerable<Widget> widgets)
        {
            this._products.Add(
                new Product()
                {
                    Name = name,
                    Amount = amount,
                    Brand = brand,
                    Category = category,
                    Collection = collection,
                    Color = color,
                    DealOfTheDayProduct = dealOfTheDayProduct,
                    Description = description,
                    Details = details,
                    DetailsLink = detailsLink,
                    DiscountProcent = discountProcent,
                    DiscountTimer = discountTimer,
                    Gender = gender,
                    Hot = hot,
                    LatestNewCollectionProduct = latestNewCollectionProduct,
                    LatestProduct = latestProduct,
                    PickedForYou = pickedForYou,
                    Price = price,
                    ProductOfTheDay = productOfTheDay,
                    Size = size,
                    State = state,
                    Rating = rating,
                    Image = image,
                    ProjectId = projectId
                });
        }
    }
}