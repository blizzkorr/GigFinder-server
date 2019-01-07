using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Models
{
    public class Host
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] ProfilePicture { get; set; }
        public int DefaultLocationId { get; set; }
        public string BackgroundColor { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual UserID UserID { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<Genre> DefaultGenres { get; set; }
        public virtual ICollection<HostSocialMedia> HostSocialMedias { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public Host()
        {
            DefaultGenres = new HashSet<Genre>();
            HostSocialMedias = new HashSet<HostSocialMedia>();
            Events = new HashSet<Event>();
            Pictures = new HashSet<Picture>();
            Reviews = new HashSet<Review>();
        }
    }

    public class HostConfiguration : IEntityTypeConfiguration<Host>
    {
        public void Configure(EntityTypeBuilder<Host> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Name).IsRequired();
            builder.Property(h => h.BackgroundColor).HasMaxLength(6).IsFixedLength().IsRequired();
            builder.Property(h => h.Timestamp).IsRowVersion();

            builder.HasOne(h => h.Location).WithOne().HasForeignKey<Host>(h => h.DefaultLocationId).IsRequired();
            builder.HasMany(h => h.DefaultGenres).WithOne().IsRequired();
        }
    }
}
