using ApplicationCore.Entities.Wallet;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.Specifications
{
    public class GetWalletByClientIdSpecification : BaseSpecification<Wallet>
    {
        public GetWalletByClientIdSpecification(string clientId)
            :base(w => w.ClientId == clientId)
        {
        }
    }
}
