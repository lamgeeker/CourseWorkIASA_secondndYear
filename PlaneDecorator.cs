using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 2. Decorator — для додавання можливостей до літака (WiFi).
namespace Planes
{
    public abstract class PlaneDecorator : Plane
    {
        protected Plane plane;

        public PlaneDecorator(Plane plane)
        {
            this.plane = plane;
            this.FlightRange = plane.FlightRange;
            this.Capacity = plane.Capacity;
            this.LoadCapacity = plane.LoadCapacity;
            this.FuelConsuption = plane.FuelConsuption;
        }
    }

    public class WifiDecorator : PlaneDecorator
    {
        public WifiDecorator(Plane plane) : base(plane) { }

        public void EnableWifi()
        {
            Console.WriteLine("WiFi увімкнено на цьому літаку.");
        }
    }

}
