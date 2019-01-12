using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Models
{
    public class EventGenre
    {
        //public int Id { get; set; }
        public int EventId { get; set; }
        public int GenreId { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Event Event { get; set; }
        public virtual Genre Genre { get; set; }

        public EventGenre() { }
    }

    public class EventGenreConfiguration : IEntityTypeConfiguration<EventGenre>
    {
        public void Configure(EntityTypeBuilder<EventGenre> builder)
        {
            builder.HasKey(eg => new { eg.EventId, eg.GenreId });

            builder.Property(eg => eg.Timestamp).IsRowVersion();

            builder.HasOne(eg => eg.Event).WithMany(e => e.EventGenres).HasForeignKey(eg => eg.EventId).IsRequired();
            builder.HasOne(eg => eg.Genre).WithMany().HasForeignKey(eg => eg.GenreId).IsRequired();
        }
    }
}
