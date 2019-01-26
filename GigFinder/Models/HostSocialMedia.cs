using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Models
{
    public class HostSocialMedia
    {
        public int HostId { get; set; }
        public int SocialMediaId { get; set; }
        public string Handle { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Host Host { get; set; }
        public virtual SocialMedia SocialMedia { get; set; }

        public HostSocialMedia() { }
    }

    public class HostSocialMediaConfiguration : IEntityTypeConfiguration<HostSocialMedia>
    {
        public void Configure(EntityTypeBuilder<HostSocialMedia> builder)
        {
            builder.HasKey(hsm => new { hsm.HostId, hsm.SocialMediaId });

            builder.Property(hsm => hsm.Handle).IsRequired();
            builder.Property(hsm => hsm.Timestamp).IsRowVersion();

            builder.HasOne(hsm => hsm.Host).WithMany(h => h.HostSocialMedias).HasForeignKey(hsm => hsm.HostId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(hsm => hsm.SocialMedia).WithMany().HasForeignKey(hsm => hsm.SocialMediaId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
