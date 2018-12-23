using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Models
{
    public class ArtistSocialMedia
    {
        public int ArtistId { get; set; }
        public int SocialMediaId { get; set; }
        public string Handle { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual SocialMedia SocialMedia { get; set; }

        public ArtistSocialMedia() { }
    }

    public class ArtistSocialMediaConfiguration : IEntityTypeConfiguration<ArtistSocialMedia>
    {
        public void Configure(EntityTypeBuilder<ArtistSocialMedia> builder)
        {
            builder.HasKey(asm => new { asm.ArtistId, asm.SocialMediaId });

            builder.Property(asm => asm.Handle).IsRequired();
            builder.Property(asm => asm.Timestamp).IsRowVersion();

            builder.HasOne(asm => asm.Artist).WithMany(a => a.ArtistSocialMedias).HasForeignKey(asm => asm.ArtistId).IsRequired();
            builder.HasOne(asm => asm.SocialMedia).WithMany().HasForeignKey(asm => asm.SocialMediaId).IsRequired();
        }
    }
}
