using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace WifiStats
{
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

            Reseau.Machines = machines;
            Reseau.IP = Network.NetworkIP;
            Console.WriteLine("Il y a " + e.Machines.Count + " machines en vie.");

            nbHostResolved = 0;
            foreach (Machine machine in machines)
            {
                Dns.BeginGetHostEntry(machine.IP, GetHostEntryCallback, machine);
            }
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

        public void GetHostEntryResult(IAsyncResult result)
        {
            Machine m = (Machine)result.AsyncState;
            String platform;

            try
            {
                // Get the results.
                IPHostEntry host = Dns.EndGetHostEntry(result);

                m.HostName = host.HostName;
                uint serverInfoResult = Network.GetServerInfo(host.HostName);
                switch (serverInfoResult)
                {
                    case 300:
                        platform = "MS-DOS";
                        break;

                    case 400:
                        platform = "OS/2";
                        break;

                    case 500:
                        platform = "Windows NT";
                        break;

                    case 600:
                        platform = "OSF";
                        break;

                    case 700:
                        platform = "VMS";
                        break;

                    default:
                        platform = "Inconnue";
                        break;
                }

                Console.WriteLine(String.Format("{0} ({1}): {2} - Plateforme : {3}", m.IP.ToString(), m.MAC, host.HostName, platform));
            }
            catch (SocketException)
            {
                Console.WriteLine(String.Format("{0} ({1}): Hôte inconnu", m.IP.ToString(), m.MAC));
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
