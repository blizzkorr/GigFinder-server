using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int HostId { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Host Host { get; set; }
        public virtual ICollection<EventGenre> EventGenres { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public ICollection<Participation> Participations { get; set; }

        public Event()
        {
            EventGenres = new HashSet<EventGenre>();
            Pictures = new HashSet<Picture>();
            Participations = new HashSet<Participation>();
        }
    }

    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title).IsRequired();
            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.Longitude).IsRequired();
            builder.Property(e => e.Latitude).IsRequired();
            builder.Property(e => e.Start).IsRequired();
            builder.Property(e => e.End).IsRequired();
            builder.Property(e => e.Timestamp).IsRowVersion();

            builder.HasOne(e => e.Host).WithMany(h => h.Events).HasForeignKey(e => e.HostId).IsRequired();
        }
    }
}
