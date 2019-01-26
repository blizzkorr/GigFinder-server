using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Models
{
    public class SearchRequestGenre
    {
        public int SearchRequestId { get; set; }
        public int GenreId { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual SearchRequest SearchRequest { get; set; }
        public virtual Genre Genre { get; set; }

        public SearchRequestGenre() { }
    }

    public class SearchRequestGenreConfiguration : IEntityTypeConfiguration<SearchRequestGenre>
    {
        public void Configure(EntityTypeBuilder<SearchRequestGenre> builder)
        {
            builder.HasKey(srg => new { srg.SearchRequestId, srg.GenreId });

            builder.Property(srg => srg.Timestamp).IsRowVersion();

            builder.HasOne(srg => srg.SearchRequest).WithMany(sr => sr.SearchRequestGenres).HasForeignKey(srg => srg.SearchRequestId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(srg => srg.Genre).WithMany().HasForeignKey(srg => srg.GenreId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
