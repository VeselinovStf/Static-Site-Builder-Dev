using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Entities.SiteType;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Entities.WidjetsEntityAggregate
{
    public class ApplicationUserWidgets : BaseEntity, IAggregateRoot
    {
            
        public string ClientId { get; set; }

       // private readonly List<Widget> _clientWidjets = new List<Widget>();

        //public IReadOnlyCollection<Widget> ClientWidjets
        //{
        //    get
        //    {
        //        return new List<Widget>(_clientWidjets.AsReadOnly());
        //    }
        //}

        public ICollection<ClientWidgets> ClientWidgets { get; set; }


        //public void AddWidjet(string name, string description, string functionality,
        //    decimal price, int version, double votes,
        //   bool isOn, bool isFree, 
        //   SiteWidgetEnum systemName, SiteWidgetEnum dependency, SiteTypesEnum siteTypeSpecification,
        //   string key, string usebleSiteTypeId)
        //{
        //    _clientWidjets.Add(new Widget()
        //    {
        //        Name = name,
        //        Description = description,
        //        CreatedOn = DateTime.Now,
        //        Dependency = dependency,
        //        Functionality = functionality,
        //        IsDeleted = IsDeleted,
        //        IsFree = isFree,
        //        IsOn = isOn,
        //        Key = key,
        //        Price = price,              
        //        SiteTypeSpecification = siteTypeSpecification,
        //        SystemName = systemName,                
        //        UsebleSiteTypeId = usebleSiteTypeId,
        //        Version = version,
        //        Votes = votes
        //    });
        //}
    }
}