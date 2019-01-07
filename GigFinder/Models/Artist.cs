using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Models
{
    public class Artist
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string BackgroundColor { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual UserID UserID { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<ArtistSocialMedia> ArtistSocialMedias { get; set; }
        public virtual ICollection<Participation> Participations { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<SearchRequest> SearchRequests { get; set; }

        public Artist()
        {
            Genres = new HashSet<Genre>();
            ArtistSocialMedias = new HashSet<ArtistSocialMedia>();
            Participations = new HashSet<Participation>();
            Pictures = new HashSet<Picture>();
            Reviews = new HashSet<Review>();
            SearchRequests = new HashSet<SearchRequest>();
        }
    }

    public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name).IsRequired();
            builder.Property(a => a.BackgroundColor).HasMaxLength(6).IsFixedLength().IsRequired();
            builder.Property(a => a.Timestamp).IsRowVersion();

            builder.HasMany(a => a.Genres).WithOne().IsRequired();
        }
    }
}
