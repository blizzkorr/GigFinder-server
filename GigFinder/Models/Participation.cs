using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Models
{
    public class Participation
    {
        public int EventId { get; set; }
        public int ArtistId { get; set; }
        public string Conditions { get; set; }
        public bool Accepted { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Event Event { get; set; }
        public virtual Artist Artist { get; set; }

        public Participation() { }
    }

    public class ParticipationConfiguration : IEntityTypeConfiguration<Participation>
    {
        public void Configure(EntityTypeBuilder<Participation> builder)
        {
            builder.HasKey(p => new { p.EventId, p.ArtistId });

            builder.Property(p => p.Accepted).HasDefaultValue(false).IsRequired();
            builder.Property(p => p.Timestamp).IsRowVersion();

            builder.HasOne(p => p.Event).WithMany(e => e.Participations).HasForeignKey(p => p.EventId).IsRequired();
            builder.HasOne(p => p.Artist).WithMany(a => a.Participations).HasForeignKey(p => p.ArtistId).IsRequired();
        }
    }
}
