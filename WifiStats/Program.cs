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
            String cmd;
            int index = 0, index2 = 0;
            Dictionary<DateTime, String> listeDeScans = new Dictionary<DateTime, String>();

            Console.WriteLine("Liste des scans : ");
            foreach (KeyValuePair<DateTime, String> dt in DataPersistance.Instance.ListScans)
            {
                listeDeScans.Add(dt.Key, dt.Value);
                Console.WriteLine(index + ": " + dt.Key);
                index++;
            }

            while (true)
            {
                Console.WriteLine("Tapez 's' pour démarrer un scan ou l'index du scan pour charger un scan : ");
                cmd = Console.ReadLine();

                if (cmd.Equals("s"))
                    break;

                try
                {
                    index2 = Int32.Parse(cmd);
                    if (index2 >= index)
                    {
                        Console.WriteLine("Cet index n'est pas disponible.");
                        continue;
                    }

                    Scan s = DataPersistance.Instance.LoadScan(listeDeScans.ElementAt(index2).Value);
                    Console.WriteLine(s.ToString());
                }
                catch (Exception)
                {
                    continue;
                }
            }

            ScanManager.Instance.startScan();
            Console.Read();
        }

        static void s_HostResolved(object sender, HostResolvedEventArgs e)
        {
            Console.WriteLine("Tous les hosts on été trouvés");
            DataPersistance.Instance.SaveScan(ScanManager.Instance.Scan);
        }
    }
}