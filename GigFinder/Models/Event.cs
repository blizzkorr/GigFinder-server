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
        public int LocationId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int HostId { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Host Host { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Participation> Participations { get; set; }

        public Event()
        {
            Genres = new HashSet<Genre>();
            Participations = new HashSet<Participation>();
        }
    }

    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            //builder.ToTable("Event");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title).IsRequired();
            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.Start).IsRequired();
            builder.Property(e => e.End).IsRequired();
            builder.Property(e => e.Timestamp).IsRowVersion();

            builder.HasOne(e => e.Host).WithMany(h => h.Events).HasForeignKey(e => e.HostId).IsRequired();
            builder.HasOne(e => e.Location).WithMany().HasForeignKey(e => e.LocationId).IsRequired();
            builder.HasMany(a => a.Genres).WithOne().IsRequired();
        }
    }
}
