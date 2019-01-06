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
                if (context.Events.Any())
                    return;

                //InitGenreAsync(context);
                //InitSocialMediaAsync(context);

                InitEventAsync(context);
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
                    GoogleIdToken = "t454151313135131",
                    BackgroundColor = "ffffff",
                    Location = new Location()
                    {
                        Country = "Germany",
                        City = "Munich",
                        ZipCode = "80000",
                        Street = "Maximiliansstraße",
                        HouseNumber = "524"
                    }
                });

                await context.SaveChangesAsync();
                host = context.Hosts.FirstOrDefault();
            }

            context.Events.Add(new Event()
            {
                Title = "München Test rockt!!",
                Description = "Alljährliches test rock",
                LocationId = host.DefaultLocationId,
                Start = new DateTime(2019, 02, 02, 18, 00, 00),
                End = new DateTime(2019, 02, 02, 23, 59, 59),
                HostId = host.Id
            });
            context.Events.Add(new Event()
            {
                Title = "München Test rockt!!",
                Description = "I don't know",
                LocationId = host.DefaultLocationId,
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

            context.Genre.Add(new Genre()
            {
                Value = "Alternative Rock",
                //SubGenres = {
                //    new Genre()
                //    {
                //        Value = "Britpop",
                //        SubGenres = { new Genre() { Value = "Post-Britpop" } }
                //    },
                //    new Genre()
                //    {
                //        Value = "Dream pop",
                //        SubGenres = { new Genre() { Value = "Shoegaze" } }
                //    },
                //    new Genre()
                //    {
                //        Value = "Grunge",
                //        SubGenres = { new Genre() { Value = "Post-grunge" } }
                //    },
                //    new Genre()
                //    {
                //        Value = "Indie rock",
                //        SubGenres =
                //        {
                //            new Genre() { Value = "Dunedin sound" },
                //            new Genre() { Value = "Math rock" },
                //            new Genre() { Value = "Post-punk revival" },
                //            new Genre() { Value = "Sadcore" },
                //            new Genre() { Value = "Slowcore" }
                //        }
                //    }
                //}
            });
            context.Genre.Add(new Genre() { Value = "Beat music" });
            context.Genre.Add(new Genre() { Value = "Christian rock" });
            context.Genre.Add(new Genre() { Value = "Dark cabaret" });
            context.Genre.Add(new Genre()
            {
                Value = "Electronic rock",
                //SubGenres = {
                //    new Genre() { Value = "Electronicore" }
                //}
            });

            await context.SaveChangesAsync();
        }

        private static async void InitSocialMediaAsync(GigFinderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));



            await context.SaveChangesAsync();
        }
    }
}
