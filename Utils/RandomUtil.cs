using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraileML.Utils
{
    public static class RandomUtil
    {
        public static double GenerateRandomDouble(double min, double max)
        {
            Random random = new Random();
            return random.NextDouble() * (max - min) + min;
        }

        public static double[] GenerateRandomArray(int n, double min, double max)
        {
            Random random = new Random();
            double[] array = new double[n];

            for (int i = 0; i < n; i++)
            {
                array[i] = random.NextDouble() * (max - min) + min;
            }

            return array;
        }
    }
}
