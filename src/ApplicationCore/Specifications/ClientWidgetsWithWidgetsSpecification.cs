using ApplicationCore.Entities.WidjetsEntityAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.Specifications
{
    public class ClientWidgetsWithWidgetsSpecification : BaseSpecification<ApplicationUserWidgets>
    {
        public ClientWidgetsWithWidgetsSpecification(string clientId)
            : base(cw => cw.ClientId == clientId)
        {
            AddInclude(cw => cw.ClientWidgets);
            
        }
    }
}
