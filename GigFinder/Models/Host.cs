﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Models
{
    public class Host
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ProfilePictureId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string BackgroundColor { get; set; }
        public byte[] Timestamp { get; set; }

        public ICollection<int> GenreIds { get; set; }

        public UserID UserId { get; set; }
        public Picture ProfilePicture { get; set; }
        public virtual ICollection<HostGenre> HostGenres { get; set; }
        public virtual ICollection<HostSocialMedia> HostSocialMedias { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public ICollection<Review> Reviews { get; set; }

        public Host()
        {
            GenreIds = new HashSet<int>();
            HostGenres = new HashSet<HostGenre>();
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

            builder.Ignore(h => h.GenreIds);

            builder.Property(h => h.Name).IsRequired();
            builder.Property(h => h.Description).IsRequired();
            builder.Property(h => h.Longitude).IsRequired();
            builder.Property(h => h.Latitude).IsRequired();
            builder.Property(h => h.BackgroundColor).HasMaxLength(6).IsFixedLength().IsRequired();
            builder.Property(h => h.Timestamp).IsRowVersion();

            builder.HasOne(h => h.ProfilePicture).WithOne().HasForeignKey<Host>(h => h.ProfilePictureId);
        }
    }
}
