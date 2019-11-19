using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace ApplicationCore.Entities.WidjetsEntityAggregate
{
    public class ClientWidjet : BaseEntity, IAggregateRoot
    {
        public string ClientId { get; set; }

        private readonly List<WidjetElement> _widjetElements = new List<WidjetElement>();

        public IReadOnlyCollection<WidjetElement> ClientWidjets
        {
            get
            {
                return new List<WidjetElement>(_widjetElements.AsReadOnly());
            }
        }

        public void AddWidjet(string name, string description, string functionality,
            decimal price, int version, double votes,
           bool isOn, bool isFree)
        {
            _widjetElements.Add(new WidjetElement()
            {
                Name = name,
                Description = description,
                Functionality = functionality,
                Price = price,
                Version = version,
                Votes = votes,
                IsOn = isOn,
                IsFree = isFree
            });
        }
    }
}