using GigFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Tools
{
    public static class DBInitializer
    {
        public static void Run()
        {
            using (var context = new GigFinderContext())
            {
                if (!context.Genres.Any())
                    InitGenreAsync(context);

                if (!context.SocialMedias.Any())
                    InitSocialMediaAsync(context);
            }
        }

        private static async void InitGenreAsync(GigFinderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            
            context.Genres.Add(new Genre() { Value = "Alternative" });
            context.Genres.Add(new Genre() { Value = "Electronic" });
            context.Genres.Add(new Genre() { Value = "Experimental" });
            context.Genres.Add(new Genre() { Value = "Hip-Hop and Rap" });
            context.Genres.Add(new Genre() { Value = "Trap" });
            context.Genres.Add(new Genre() { Value = "Pop" });
            context.Genres.Add(new Genre() { Value = "R&B" });
            context.Genres.Add(new Genre() { Value = "Latino" });
            context.Genres.Add(new Genre() { Value = "Rock" });
            context.Genres.Add(new Genre() { Value = "Punk" });
            context.Genres.Add(new Genre() { Value = "Metal" });
            context.Genres.Add(new Genre() { Value = "Jazz" });
            context.Genres.Add(new Genre() { Value = "Folk" });
            context.Genres.Add(new Genre() { Value = "Techno" });
            context.Genres.Add(new Genre() { Value = "House" });
            context.Genres.Add(new Genre() { Value = "Singer Songwriter" });

            await context.SaveChangesAsync();
        }

        private static async void InitSocialMediaAsync(GigFinderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            context.SocialMedias.Add(new SocialMedia() { Name = "Soundcloud", Website = "https://soundcloud.com/" });
            context.SocialMedias.Add(new SocialMedia() { Name = "Facebook", Website = "https://www.facebook.com/" });
            context.SocialMedias.Add(new SocialMedia() { Name = "Twitter", Website = "https://twitter.com/" });
            context.SocialMedias.Add(new SocialMedia() { Name = "YouTube", Website = "https://www.youtube.com/" });
            context.SocialMedias.Add(new SocialMedia() { Name = "MySpace", Website = "https://myspace.com/" });
            context.SocialMedias.Add(new SocialMedia() { Name = "last.fm", Website = "https://www.last.fm/" });
            context.SocialMedias.Add(new SocialMedia() { Name = "Spotify", Website = "https://www.spotify.com/" });
            context.SocialMedias.Add(new SocialMedia() { Name = "Website", Website = "unset" });

            await context.SaveChangesAsync();
        }
    }
}
