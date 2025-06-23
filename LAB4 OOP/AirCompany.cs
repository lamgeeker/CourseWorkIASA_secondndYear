using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planes;
namespace airCompany
{
    class AirCompany
    {


        public void CountCapAndLoad(Plane[] planes)
        {
            int AllCap = 0;
            int AllLoad = 0;
            for (int i = 0; i < planes.Length; i++)
            {
                AllCap += planes[i].Capacity;
                AllLoad += planes[i].LoadCapacity;
            }
            Console.WriteLine($"Загальна мiсткiсть всiх лiтакiв компанiї - {AllCap}   |||  Загальна вантажопiдйомнiсть всiх лiтакiв компанiї {AllLoad} ");
        }

        public void SortByFlight(Plane[] planes)
        {
            Array.Sort(planes);
            foreach (Plane plane in planes)
            {
                Console.WriteLine($"Лiтак {plane} має дальнiсть польоту {plane.FlightRange}");
            }
        }

        public void PlaneByFuel(Plane[] planes, int a, int b)
        {
            for (int i = 0; i < planes.Length; i++)
            {
                if (planes[i].FuelConsuption > a && planes[i].FuelConsuption < b)
                {
                    Console.WriteLine(planes[i] + $" має споживання пального мiж {a} i {b} ({planes[i].FuelConsuption})");
                }
                else
                {
                    Console.WriteLine($"Лiтак {planes[i]} має iнший показник мпоживання пального, нiж той, що ви вказали");

                }
            }
        }

    }
}