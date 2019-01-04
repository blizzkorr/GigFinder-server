using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Models
{
    [TypeConverter(typeof(GeoPointConverter))]
    public class GeoPoint
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public GeoPoint() { }

        public GeoPoint(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        public static bool TryParse(string s, out GeoPoint result)
        {
            result = null;

            var parts = s.Split(',');
            if (parts.Length != 2)
                return false;

            if (double.TryParse(parts[0], out double latitude) && double.TryParse(parts[1], out double longitude))
            {
                result = new GeoPoint() { Longitude = longitude, Latitude = latitude };
                return true;
            }
            return false;
        }

        public static double CalculateDistance(GeoPoint pointA, GeoPoint pointB)
        {
            double theDistance = (Math.Sin(ConvertToRadians(pointA.Latitude)) * Math.Sin(ConvertToRadians(pointB.Latitude)) +
                    Math.Cos(ConvertToRadians(pointA.Latitude)) * Math.Cos(ConvertToRadians(pointB.Latitude)) *
                    Math.Cos(ConvertToRadians(pointA.Longitude - pointB.Longitude)));

            return ConvertToRadians(Math.Acos(theDistance)) * 69.09D * 1.6093D;
        }

        public static double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }

    public class GeoPointConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
                if (GeoPoint.TryParse((string)value, out GeoPoint point))
                    return point;
            return base.ConvertFrom(context, culture, value);
        }
    }
}
