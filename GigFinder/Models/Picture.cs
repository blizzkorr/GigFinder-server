using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Models
{
    public class Picture
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public int? ArtistId { get; set; }
        public int? HostId { get; set; }
        public int? EventId { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual Host Host { get; set; }
        public virtual Event Event { get; set; }

        public Picture() { }
    }

    public class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Image).IsRequired();
            builder.Property(p => p.Timestamp).IsRowVersion();

            builder.HasOne(p => p.Artist).WithMany(a => a.Pictures).HasForeignKey(p => p.ArtistId);
            builder.HasOne(p => p.Host).WithMany(a => a.Pictures).HasForeignKey(p => p.HostId);
            builder.HasOne(p => p.Event).WithMany(a => a.Pictures).HasForeignKey(p => p.EventId);
        }
    }
}