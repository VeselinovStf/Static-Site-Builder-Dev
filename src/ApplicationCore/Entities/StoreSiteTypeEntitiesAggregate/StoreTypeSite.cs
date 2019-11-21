﻿using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Entities.SiteType;
using ApplicationCore.Entities.WidjetsEntityAggregate;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate
{
    public class StoreTypeSite : DescriptiveEntity, IBaseSiteProject, IAggregateRoot
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "New Project Location")]
        public string NewProjectLocation { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Template Location")]
        public string TemplateLocation { get; set; }

        public string ClientId { get; set; }

        public LaunchConfig LaunchingConfig { get; set; }

        private readonly List<Product> _products = new List<Product>();

        public IReadOnlyCollection<Product> Products
        {
            get
            {
                return new List<Product>(_products.AsReadOnly());
            }
        }

        //Build in widjets
        public ICollection<Widjet> TemplateUsableWidjets { get; set; }

        //Build in site type config
        public SiteTypes SiteTypeSpecification
        {
            get
            {
                return SiteTypes.StoreType;
            }
        }

        public void AddProduct(string projectId, string name, string description,
            bool hot, bool latestNewCollectionProduct, bool latestProduct,
            bool pickedForYou, decimal price, string size, bool state,
            string gender, int amount, string brand, string category,
            string collection, string color, bool dealOfTheDayProduct,
            string details, string detailsLink, decimal discountProcent,
            DateTime discountTimer, bool productOfTheDay,
            double rating, string image)
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