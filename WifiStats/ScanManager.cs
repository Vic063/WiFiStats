using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WifiStats
{
    public class ScanManager
    {
        

        public void startScan()
        {
            Scan scan = new Scan();

            scan.HostResolved += s_HostResolved;
            scan.startScan();

            this.Scans.Add(scan);
        }

        private void s_HostResolved(object sender, HostResolvedEventArgs e)
        {
            DataPersistance.SaveScan(Scans.Last());
            Console.WriteLine("Récupération des hosts terminée.");
        }

        private ScanManager()
        {
            this.Scans = new List<Scan>();
            this.DataPersistance = new DataPersistance();
        }

        private static ScanManager _instance = null ;
        public static ScanManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScanManager();
                }

                return _instance;
            }
        }

        public List<Scan> Scans
        {
            get;
            private set;
        }

        public DataPersistance DataPersistance
        {
            get;
            private set;
        }

        public DiagramBuilder DiagramBuilder
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
