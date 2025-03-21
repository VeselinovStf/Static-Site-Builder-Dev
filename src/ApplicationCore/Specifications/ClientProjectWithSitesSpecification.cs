﻿using ApplicationCore.Entities.SiteProjectAggregate;

namespace ApplicationCore.Specifications
{
    public class ClientProjectWithSitesSpecification : BaseSpecification<Project>
    {
        public ClientProjectWithSitesSpecification(string clientId)
            : base(p => p.ClientId == clientId)
        {
            AddInclude(p => p.BlogSiteTypes);
            AddInclude(p => p.StoreSiteTypes);
        }
    }
}