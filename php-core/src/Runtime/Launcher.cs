using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using PHP.Core.Memory.Data;

namespace PHP.Runtime
{
    public class Launcher
    {
        public static void Main(string[] args)
        {
            MemoryData a = new MemoryString("Hello world!");
            Console.WriteLine(a.ToString());
            MemoryData b = a.ToMemoryArray();
            b.ToMemoryArray().Set(a, new MemoryInteger(-325));
            Console.WriteLine(b.ToString());

            Console.WriteLine("End.");
            Console.ReadKey();
        }
    }
}

