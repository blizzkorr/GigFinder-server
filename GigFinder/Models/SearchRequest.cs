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
        public int LocationId { get; set; }
        public double Radius { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }

        public SearchRequest() 
        {
            Genres = new HashSet<Genre>();
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
            builder.Property(sr => sr.Timestamp).IsRowVersion();

            builder.HasOne(sr => sr.Artist).WithMany(a => a.SearchRequests).HasForeignKey(sr => sr.ArtistId).IsRequired();
            builder.HasOne(sr => sr.Location).WithMany().HasForeignKey(sr => sr.LocationId).IsRequired();
            builder.HasMany(a => a.Genres).WithOne().IsRequired();
        }
    }
}