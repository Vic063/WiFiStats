using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WifiStats
{
    class Program
    {
        static void Main(string[] args)
        {
            Scan s = new Scan();
            s.startScan();
            

            

            Console.Read();
        }
    }
}
