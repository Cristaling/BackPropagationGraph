using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackPropagationGraph
{
    class MathUtility
    {

        static Random rand = new Random();

        public static double signoid(double a)
        {
            return 1 / (1 + Math.Pow(Math.E, a));
        }

        public static double getRandomWeight()
        {
            return rand.NextDouble() * 2 - 1;
        }

    }
}
