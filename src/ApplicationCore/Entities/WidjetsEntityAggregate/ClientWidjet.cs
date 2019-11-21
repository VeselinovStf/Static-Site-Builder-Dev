using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace ApplicationCore.Entities.WidjetsEntityAggregate
{
    public class ClientWidjet : BaseEntity, IAggregateRoot
    {
        public string ClientId { get; set; }

        private readonly List<WidjetElement> _clientWidjets = new List<WidjetElement>();

        public IReadOnlyCollection<WidjetElement> ClientWidjets
        {
            get
            {
                return new List<WidjetElement>(_clientWidjets.AsReadOnly());
            }
        }

        public void AddWidjet(string name, string description, string functionality,
            decimal price, int version, double votes,
           bool isOn, bool isFree)
        {
            _clientWidjets.Add(new WidjetElement()
            {
            });
        }
    }
}