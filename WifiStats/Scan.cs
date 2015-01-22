using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WifiStats
{
    public class Scan
    {
        public Scan()
        {
            this.Date = DateTime.Now;
            this.Network = new Network();
        }

        public void startScan()
        {
            Network.pingFinished += Network_pingFinished;
            Network.ScanNetwork();
        }

        void Network_pingFinished(object sender, PingFinishedEventArgs e)
        {
            Console.WriteLine("Il y a " + e.Machines.Count + " machines en vie.");
        }

        public DateTime Date
        {
            get;
            private set;
        }

        public EtatScan Etat
        {
            get;
            private set;
        }

        public Reseau Reseau
        {
            get;
            private set;
        }

        public Network Network
        {
            get;
            private set;
        }
    }
}
