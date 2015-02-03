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
            ScanManager.Instance.startScan();



            Console.Read();

            foreach (DateTime dt in DataPersistance.Instance.ListScans)
            {
                Console.WriteLine(dt);
            }
        }

        static void s_HostResolved(object sender, HostResolvedEventArgs e)
        {
            Console.WriteLine("Tous les hosts on été trouvés");
        }
    }
}