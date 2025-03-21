﻿using ApplicationCore.Entities.MessageAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(m => m.From)
                   .HasMaxLength(100)
                   .HasColumnName("Sender")
                   .IsRequired();

            builder.Property(m => m.To)
                   .HasMaxLength(100)
                   .HasColumnName("To")
                   .IsRequired();

            builder.Property(m => m.Subject)
                   .HasMaxLength(100)
                   .HasColumnName("Subject")
                   .IsRequired();

            builder.Property(m => m.Text)
                   .HasMaxLength(2000)
                   .HasColumnName("Content")
                   .IsRequired();

            builder.Property(m => m.SendDate)
                   .HasColumnName("Send Date Of Message")
                   .IsRequired();
        }
    }
}