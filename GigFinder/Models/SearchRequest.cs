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
        public virtual ICollection<SearchRequestGenre> SearchRequestGenres { get; set; }

        public SearchRequest() 
        {
            SearchRequestGenres = new HashSet<SearchRequestGenre>();
        }

        public bool IsEventInRadius(Event @event)
        {
            if (this == null)
                throw new ArgumentNullException();
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));

            return Radius <= Location.ComputeDistance(@event.Location);
        }
    }

    public class SearchRequestConfiguration : IEntityTypeConfiguration<SearchRequest>
    {
        public void Configure(EntityTypeBuilder<SearchRequest> builder)
        {
            builder.HasKey(sr => sr.Id);

            builder.Property(sr => sr.Radius).IsRequired();
            builder.Property(sr => sr.Longitude).IsRequired();
            builder.Property(sr => sr.Latitude).IsRequired();
            builder.Property(sr => sr.Timestamp).IsRowVersion();

            builder.HasOne(sr => sr.Artist).WithMany(a => a.SearchRequests).HasForeignKey(sr => sr.ArtistId).IsRequired();
        }
    }
}