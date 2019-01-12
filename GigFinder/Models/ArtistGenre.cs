using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Models
{
    public class ArtistGenre
    {
        //public int Id { get; set; }
        public int ArtistId { get; set; }
        public int GenreId { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual Genre Genre { get; set; }

        public ArtistGenre() { }
    }

    public class ArtistGenreConfiguration : IEntityTypeConfiguration<ArtistGenre>
    {
        public void Configure(EntityTypeBuilder<ArtistGenre> builder)
        {
            builder.HasKey(ag => new { ag.ArtistId, ag.GenreId });

            builder.Property(ag => ag.Timestamp).IsRowVersion();

            builder.HasOne(ag => ag.Artist).WithMany(a => a.ArtistGenres).HasForeignKey(ag => ag.ArtistId).IsRequired();
            builder.HasOne(ag => ag.Genre).WithMany().HasForeignKey(ag => ag.GenreId).IsRequired();
        }
    }
}
