using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public int HostId { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual Host Host { get; set; }

        public Favorite() { }
    }

    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Timestamp).IsRowVersion();

            builder.HasOne(f => f.Artist).WithMany(a => a.Favorites).HasForeignKey(f => f.ArtistId).IsRequired();
            builder.HasOne(f => f.Host).WithMany().HasForeignKey(f => f.HostId).IsRequired();
        }
    }
}
