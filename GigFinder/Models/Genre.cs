using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int? ParentId { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Genre Parent { get; set; }
        public virtual ICollection<Genre> SubGenres { get; set; }

        public Genre()
        {
            SubGenres = new HashSet<Genre>();
        }
    }

    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Value).IsRequired();
            builder.Property(g => g.Timestamp).IsRowVersion();

            builder.HasOne(g => g.Parent).WithMany(p => p.SubGenres).HasForeignKey(g => g.ParentId);
        }
    }
}