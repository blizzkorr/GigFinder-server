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
                if (!context.Events.Any())
                    InitEventAsync(context);

                if (!context.Genres.Any())
                    InitGenreAsync(context);

                if (!context.SocialMedias.Any())
                    InitSocialMediaAsync(context);
            }
        }

        private static async void InitEventAsync(GigFinderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            Host host = context.Hosts.FirstOrDefault();
            if (host == null)
            {
                context.Hosts.Add(new Host()
                {
                    Name = "Test Host",
                    Description = "Best club in Munich",
                    UserId = new UserID()
                    {
                        GoogleIdToken = "t454151313135131"
                    },
                    BackgroundColor = "ffffff",
                    Longitude = 48.1548895,
                    Latitude = 11.4717964
                });

                await context.SaveChangesAsync();
                host = context.Hosts.FirstOrDefault();
            }

            context.Events.Add(new Event()
            {
                Title = "München Test rockt!!",
                Description = "Alljährliches test rock",
                Longitude = 48.1548895,
                Latitude = 11.4717964,
                Start = new DateTime(2019, 02, 02, 18, 00, 00),
                End = new DateTime(2019, 02, 02, 23, 59, 59),
                HostId = host.Id
            });
            context.Events.Add(new Event()
            {
                Title = "München Test rockt!!",
                Description = "I don't know",
                Longitude = 48.1548895,
                Latitude = 11.4717964,
                Start = new DateTime(2019, 01, 31, 14, 00, 00),
                End = new DateTime(2019, 01, 31, 22, 00, 00),
                HostId = host.Id
            });

            await context.SaveChangesAsync();
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
