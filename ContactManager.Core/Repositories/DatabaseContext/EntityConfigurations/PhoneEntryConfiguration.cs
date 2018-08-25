using System;
using System.Collections.Generic;
using System.Text;
using ContactManager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactManager.Core.Repositories.DatabaseContext.EntityConfigurations
{
    public class PhoneEntryConfiguration : IEntityTypeConfiguration<PhoneEntry>
    {
        public void Configure(EntityTypeBuilder<PhoneEntry> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(50);
        }
    }
}
