using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using PHP.Runtime.Memory;
using PHP.Runtime.Memory.Data;

namespace PHP.Runtime
{
    public class Launcher
    {
        public static void Main(string[] args)
        {
            MemoryBlock a = new MemoryBlock();
            MemoryBlock b = new MemoryBlock(a);
            a.SetData("$a", new MemoryBoolean(true));
            b.SetData("$b", new MemoryBoolean(true));
            a.SetData("$a1", new MemoryInteger(13));
            a.SetData("$a2", new MemoryInteger(14));
            a.SetData("$a3", new MemoryInteger(15));
            b.SetData("$a4", new MemoryInteger(16));
            b.SetData("$a2", new MemoryInteger(17));

            Console.WriteLine("End.");
            Console.ReadKey();
        }
    }
}

