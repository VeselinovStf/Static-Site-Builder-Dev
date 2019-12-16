using ApplicationCore.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities.Wallet
{
    public class Wallet : BaseEntity
    {
        public string ClientId { get; set; }

        public decimal AvailibleCredit { get; set; }

        public decimal AvailibleDiamons { get; set; }
    }
}
