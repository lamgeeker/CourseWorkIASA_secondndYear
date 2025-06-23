using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions
{
    public class EmptyPlaneSetException : Exception
    {
        public EmptyPlaneSetException()
            : base("Множина літаків порожня. Завантажте або додайте літаки спочатку.") { }
    }

    public class InvalidFuelRangeException : Exception
    {
        public InvalidFuelRangeException()
            : base("Некоректний діапазон пального. A має бути менше B.") { }
    }

}
