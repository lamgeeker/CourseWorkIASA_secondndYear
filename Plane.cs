using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Planes
{

    public class Plane : Flyer, IComparable
    {
        public int FlightRange { get; set; }
        public int Capacity { get; set; }
        public int LoadCapacity { get; set; }
        public int FuelConsuption { get; set; }
        public virtual string TypeName => GetType().Name;
        public int CompareTo(object obj)
        {
            Plane plane = obj as Plane;
            return this.FlightRange.CompareTo(plane.FlightRange);
        }
        public override bool Equals(object obj)
        {
            if (obj is not Plane other) return false;
            return FlightRange == other.FlightRange &&
                   Capacity == other.Capacity &&
                   LoadCapacity == other.LoadCapacity &&
                   FuelConsuption == other.FuelConsuption;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FlightRange, Capacity, LoadCapacity, FuelConsuption);
        }

    }
}