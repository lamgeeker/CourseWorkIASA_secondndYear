using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 1. Factory Method — для створення літаків різних типів.
namespace Planes
{
    public abstract class PlaneFactory
    {
        public abstract Plane CreatePlane(int flightRange, int capacity, int loadCapacity, int fuelConsumption);
    }

    public class BoeingFactory : PlaneFactory
    {
        public override Plane CreatePlane(int flightRange, int capacity, int loadCapacity, int fuelConsumption)
        {
            return new Boeing
            {
                FlightRange = flightRange,
                Capacity = capacity,
                LoadCapacity = loadCapacity,
                FuelConsuption = fuelConsumption
            };
        }
    }

    public class MriyaFactory : PlaneFactory
    {
        public override Plane CreatePlane(int flightRange, int capacity, int loadCapacity, int fuelConsumption)
        {
            return new Mriya
            {
                FlightRange = flightRange,
                Capacity = capacity,
                LoadCapacity = loadCapacity,
                FuelConsuption = fuelConsumption
            };
        }
    }
    public class AirbusA330Factory : PlaneFactory
    {
        public override Plane CreatePlane(int flightRange, int capacity, int loadCapacity, int fuelConsumption)
        {
            return new AirBusA330
            {
                FlightRange = flightRange,
                Capacity = capacity,
                LoadCapacity = loadCapacity,
                FuelConsuption = fuelConsumption
            };
        }
    }
    public class Boeing757Factory : PlaneFactory
    {
        public override Plane CreatePlane(int flightRange, int capacity, int loadCapacity, int fuelConsumption)
        {
            return new Boeing757
            {
                FlightRange = flightRange,
                Capacity = capacity,
                LoadCapacity = loadCapacity,
                FuelConsuption = fuelConsumption
            };
        }
    }
}

