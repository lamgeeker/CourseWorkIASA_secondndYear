using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB4_OOP
{
    public class IntegralCalculator
    {
        public double Calculate(double a, double b, long n, Function f, int threadCount)
        {
            double h = (b - a) / n;
            double sum = 0;
            object lockObj = new object();

            Parallel.For(0, threadCount, i =>
            {
                long from = i * n / threadCount;
                long to = (i + 1) * n / threadCount;
                double localSum = 0;
                for (long j = from; j < to; j++)
                {
                    double x = a + j * h;
                    localSum += f.Calculate(x);
                }

                lock (lockObj)
                {
                    sum += localSum;
                }
            });

            return sum * h;
        }
    }
}
