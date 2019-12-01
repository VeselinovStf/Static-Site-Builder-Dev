using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Entities.WidjetsEntityAggregate
{
    public class ClientWidjet : BaseEntity, IAggregateRoot
    {
        public ClientWidjet()
        {

        }
        public ClientWidjet(IList<Widjet> userDefaultWidjets)
        {
            this._clientWidjets = userDefaultWidjets.ToList();
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
           bool isOn, bool isFree)
        {
            _clientWidjets.Add(new Widjet()
            {
            });
        }
    }
}