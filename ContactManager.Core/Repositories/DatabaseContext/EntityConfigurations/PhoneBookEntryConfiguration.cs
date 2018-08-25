using ContactManager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactManager.Core.Repositories.DatabaseContext.EntityConfigurations
{
    public class PhoneBookEntryConfiguration : IEntityTypeConfiguration<PhoneBookEntry>
    {
        public void Configure(EntityTypeBuilder<PhoneBookEntry> builder)
        {
            builder.HasKey(q =>
                new {
                    q.PhoneBookId,
                    q.PhoneEntryId
                });

            builder.HasOne(tm => tm.PhoneBook)
                .WithMany(t => t.BookEntries)
                .HasForeignKey(tm => tm.PhoneBookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(tm => tm.PhoneEntry) 
                .WithMany(m => m.BookEntries)
                .HasForeignKey(tm => tm.PhoneEntryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
