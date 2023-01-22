using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extension
{
    public class Randomizer
    {
        private static Random random = new Random();
        public static int RandomFromArray(params int[] arr)
        {
            return random.Next(arr.Length);
        }
    }
}
