using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Entities.SiteType;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Entities.WidjetsEntityAggregate
{
    public class ClientWidjet : BaseEntity, IAggregateRoot
    {
        public ClientWidjet()
        {

        }
     
        public string ClientId { get; set; }

        private readonly List<Widjet> _clientWidjets = new List<Widjet>();

        public IReadOnlyCollection<Widjet> ClientWidjets
        {
            get
            {
                return new List<Widjet>(_clientWidjets.AsReadOnly());
            }
        }

        public void AddWidjet(string name, string description, string functionality,
            decimal price, int version, double votes,
           bool isOn, bool isFree, 
           SiteWidjetEnum systemName, SiteWidjetEnum dependency, SiteTypesEnum siteTypeSpecification,
           string key, string usebleSiteTypeId)
        {
            _clientWidjets.Add(new Widjet()
            {
                Name = name,
                Description = description,
                CreatedOn = DateTime.Now,
                Dependency = dependency,
                Functionality = functionality,
                IsDeleted = IsDeleted,
                IsFree = isFree,
                IsOn = isOn,
                Key = key,
                Price = price,              
                SiteTypeSpecification = siteTypeSpecification,
                SystemName = systemName,                
                UsebleSiteTypeId = usebleSiteTypeId,
                Version = version,
                Votes = votes
            });
        }
    }
}