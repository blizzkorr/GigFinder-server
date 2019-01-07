using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Models
{
    public class SearchRequest
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Radius { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }

        public SearchRequest() 
        {
            Genres = new HashSet<Genre>();
        }
    }

    public class SearchRequestConfiguration : IEntityTypeConfiguration<SearchRequest>
    {
        public void Configure(EntityTypeBuilder<SearchRequest> builder)
        {
            builder.HasKey(sr => sr.Id);

            builder.Property(sr => sr.Radius).IsRequired();
            builder.Property(e => e.Longitude).IsRequired();
            builder.Property(e => e.Latitude).IsRequired();
            builder.Property(sr => sr.Timestamp).IsRowVersion();

            builder.HasOne(sr => sr.Artist).WithMany(a => a.SearchRequests).HasForeignKey(sr => sr.ArtistId).IsRequired();
            builder.HasMany(a => a.Genres).WithOne().IsRequired();
        }
    }
}