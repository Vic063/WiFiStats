using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WifiStats
{
    class Program
    {
        static void Main(string[] args)
        {
            Scan s = new Scan();
            s.startScan();
            s.HostResolved += s_HostResolved;

            Console.Read();
        }

        static void s_HostResolved(object sender, HostResolvedEventArgs e)
        {
            Console.WriteLine("Tous les hosts on été trouvés");
        }
    }
}