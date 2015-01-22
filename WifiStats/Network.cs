using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Linq;
using System.Net.NetworkInformation;

namespace WifiStats
{
    public class Network
    {
        private List<Machine> listMachines;
        private IPAddress localAddr;
        private int nbProcessed = 0;
        public event EventHandler<PingFinishedEventArgs> pingFinished ;

        public Network()
        {
            listMachines = new List<Machine>();
            localAddr = Dns.GetHostAddresses(Dns.GetHostName()).Where(ip => ip.AddressFamily.ToString().Equals("InterNetwork")).ElementAt(0);
        }

        public void ScanNetwork()
        {
            Byte[] networkAddress = localAddr.GetAddressBytes();
            IPAddress ipMachine;

            for (Byte m = 1; m < 255; ++m)
            {
                networkAddress[3] = m;
                ipMachine = new IPAddress(networkAddress);

                Ping p = new Ping();

                p.PingCompleted += p_PingCompleted;
                p.SendAsync(ipMachine, null);
            }
        }

        void p_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            Machine machine;

            switch (e.Reply.Status)
            {
                case IPStatus.Success:
                    machine = new Machine();
                    machine.IP = e.Reply.Address;

                    listMachines.Add(machine);
                    Console.WriteLine(String.Format("{0} I'm alive!", e.Reply.Address.ToString()));
                    break;
            }
            nbProcessed++;
            if (nbProcessed == 254)
            {
                OnPingFinished(new PingFinishedEventArgs(listMachines));
            }
        }
        
        protected virtual void OnPingFinished(PingFinishedEventArgs args)
        {
            if (pingFinished != null)
            {
                pingFinished(this, args);
            }
        }
    }
}
