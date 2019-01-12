using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Models
{
    public class HostGenre
    {
        public int HostId { get; set; }
        public int GenreId { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Host Host { get; set; }
        public virtual Genre Genre { get; set; }

        public HostGenre() { }
    }

    public class HostGenreConfiguration : IEntityTypeConfiguration<HostGenre>
    {
        public void Configure(EntityTypeBuilder<HostGenre> builder)
        {
            builder.HasKey(hg => new { hg.HostId, hg.GenreId });

            builder.Property(hg => hg.Timestamp).IsRowVersion();

            builder.HasOne(hg => hg.Host).WithMany(h => h.HostGenres).HasForeignKey(hg => hg.HostId).IsRequired();
            builder.HasOne(hg => hg.Genre).WithMany().HasForeignKey(hg => hg.GenreId).IsRequired();
        }
    }
}
