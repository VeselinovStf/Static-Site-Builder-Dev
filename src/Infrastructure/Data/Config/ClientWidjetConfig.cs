﻿using ApplicationCore.Entities.SitesTemplates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ClientWidjetConfig : IEntityTypeConfiguration<SiteTemplate>
    {
        public void Configure(EntityTypeBuilder<SiteTemplate> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(SiteTemplate.SiteTemplateElements));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}