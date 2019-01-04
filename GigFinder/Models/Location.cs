using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Models
{
    public class Location 
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string AddressAddition { get; set; }
        public byte[] Timestamp { get; set; }

        public Location() { }

        public double ComputeDistance(Location otherLocation)
        {
            return 0;
        }
    }

    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Timestamp).IsRowVersion();
        }
    }
}