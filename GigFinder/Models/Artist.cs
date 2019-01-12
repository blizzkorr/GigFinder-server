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
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ProfilePictureId { get; set; }
        public string BackgroundColor { get; set; }
        public byte[] Timestamp { get; set; }

        public ICollection<int> GenreIds { get; set; }

        public virtual UserID UserId { get; set; }
        public virtual Picture ProfilePicture { get; set; }
        public virtual ICollection<ArtistGenre> ArtistGenres { get; set; }
        public virtual ICollection<ArtistSocialMedia> ArtistSocialMedias { get; set; }
        public virtual ICollection<Participation> Participations { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<SearchRequest> SearchRequests { get; set; }

        public Artist()
        {
            GenreIds = new HashSet<int>();
            ArtistGenres = new HashSet<ArtistGenre>();
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

            builder.Ignore(a => a.GenreIds);

            builder.Property(a => a.Name).IsRequired();
            builder.Property(a => a.Description).IsRequired();
            builder.Property(a => a.BackgroundColor).HasMaxLength(6).IsFixedLength().IsRequired();
            builder.Property(a => a.Timestamp).IsRowVersion();

            builder.HasOne(a => a.ProfilePicture).WithOne().HasForeignKey<Artist>(a => a.ProfilePictureId);
        }
    }
}
