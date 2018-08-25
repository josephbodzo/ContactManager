using ContactManager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactManager.Core.Repositories.DatabaseContext.EntityConfigurations
{
    public class PhoneBookConfiguration: IEntityTypeConfiguration<PhoneBook>
    {
        public void Configure(EntityTypeBuilder<PhoneBook> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(50);
        }
    }
}
