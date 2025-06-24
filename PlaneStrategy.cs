using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planes;
// 3. Strategy — для сортування і фільтрації літаків за різними критеріями.
namespace airCompany
{
    public interface IPlaneStrategy
    {
        void Execute(Plane[] planes);
    }

    public class SortByRangeStrategy : IPlaneStrategy
    {
        public void Execute(Plane[] planes)
        {
            Array.Sort(planes);
            foreach (var plane in planes)
                Console.WriteLine($"Сортування: {plane.FlightRange}");
        }
    }

    public class FilterByFuelStrategy : IPlaneStrategy
    {
        private int min, max;
        public FilterByFuelStrategy(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        public void Execute(Plane[] planes)
        {
            foreach (var plane in planes)
            {
                if (plane.FuelConsuption > min && plane.FuelConsuption < max)
                    Console.WriteLine($"Знайдено літак: {plane.FuelConsuption}");
            }
        }
    }

    public class PlaneContext
    {
        private IPlaneStrategy strategy;

        public void SetStrategy(IPlaneStrategy strategy)
        {
            this.strategy = strategy;
        }

        public void ExecuteStrategy(Plane[] planes)
        {
            strategy.Execute(planes);
        }
    }

}
