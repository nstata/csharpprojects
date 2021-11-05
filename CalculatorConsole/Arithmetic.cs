using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorConsole
{
    public class Arithmetic
    {
        public double Add(double d1, double d2)
        {
            return d1 + d2;
        }
        public double Sub(double d1, double d2)
        {
            return d1 - d2;
        }
        public double Mult(double d1, double d2)
        {
            return d1 * d2;
        }
        public double Div(double d1, double d2)
        {
            if (d2 == 0)
                throw new DivideByZeroException("Denom is zero");

            return d1 / d2;
        }

    }
}
