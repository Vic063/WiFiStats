using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;

namespace WifiStats
{
    [DataContract]
    public class Scan
    {

        private int nbHostResolved = 0;

        public event EventHandler<HostResolvedEventArgs> HostResolved;

        public Scan()
        {
            this.Date = DateTime.Now;
            this.Network = new Network();
            this.Reseau = new Reseau();
        }

        public void startScan()
        {
            Network.pingFinished += Network_pingFinished;
            Network.ScanNetwork();
        }

        void Network_pingFinished(object sender, PingFinishedEventArgs e)
        {
            List<Machine> machines = Network.Machines;
            AsyncCallback GetHostEntryCallback = new AsyncCallback(GetHostEntryResult);

            Console.WriteLine("Il y a " + e.Machines.Count + " machines en vie.");

            nbHostResolved = 0;

            Reseau.Machines = machines;

            foreach (Machine machine in machines)
            {
                Dns.BeginGetHostEntry(machine.IP, GetHostEntryCallback, machine);
            }

        }

        [DataMember]
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
        [DataMember]
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

        public void GetHostEntryResult(IAsyncResult result)
        {
            Machine m = (Machine)result.AsyncState;

            try
            {
                // Get the results.
                IPHostEntry host = Dns.EndGetHostEntry(result);
                Console.WriteLine(String.Format("{0}: {1}", m.IP.ToString(), host.HostName));

                m.HostName = host.HostName;

            }
            catch (SocketException e)
            {
                Console.WriteLine(String.Format("{0}: Hôte inconnu", m.IP.ToString()));
            }
            finally
            {

                nbHostResolved++;

                if (nbHostResolved == Network.Machines.Count)
                {
                    OnHostResolved(new HostResolvedEventArgs(Network.Machines));
                }
            }
        }

        protected virtual void OnHostResolved(HostResolvedEventArgs args)
        {
            if (HostResolved != null)
            {
                HostResolved(this, args);
            }
        }

    }
}
