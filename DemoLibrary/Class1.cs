using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary
{
    public class Calculator
    {
        public int Add(int a, int b)
        {
            var calc = new ActualCalculator();
            var sum = calc.Add(a, b);
            return sum;
        }
    }

    internal class ActualCalculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}
