using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Threading;

namespace WifiStats
{
    public class Network
    {
        private List<Machine> listMachines;
        private IPAddress localAddr;
        private int nbProcessed = 0;
        public event EventHandler<PingFinishedEventArgs> pingFinished;
        static object locker = new object();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct SERVER_INFO_100
        {
            public uint PlatformId;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string Name;
        }

        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        static extern uint SendARP(uint DestIP, uint SrcIP, byte[] pMacAddr, ref ulong PhyAddrLen);

        [DllImport("Netapi32.dll", CharSet = CharSet.Auto)]
        static extern uint NetServerGetInfo([MarshalAs(UnmanagedType.LPTStr)] String servername, uint level, out IntPtr bufptr);

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
            Monitor.Enter(locker);
            Machine machine;

            switch (e.Reply.Status)
            {
                case IPStatus.Success:
                    PhysicalAddress mac = GetMacAddress(e.Reply.Address);

                    machine = new Machine();
                    machine.IP = e.Reply.Address;
                    machine.MAC = mac.ToString();

                    listMachines.Add(machine);
                    Console.WriteLine(String.Format("{0} I'm alive!", e.Reply.Address.ToString()));
                    break;
            }

            nbProcessed++;
            if (nbProcessed == 254)
            {
                OnPingFinished(new PingFinishedEventArgs(listMachines));
            }
            Monitor.Exit(locker);
        }
        
        protected virtual void OnPingFinished(PingFinishedEventArgs args)
        {
            if (pingFinished != null)
            {
                pingFinished(this, args);
            }
        }

        public List<Machine> Machines
        {
            get
            {
                return listMachines;
            }
        }

        public IPAddress NetworkIP
        {
            get
            {
                UInt32 ip = BitConverter.ToUInt32(localAddr.GetAddressBytes(), 0);
                return new IPAddress(ip);
            }
        }

        private PhysicalAddress GetMacAddress(IPAddress ipAddress)
        {
            const int MacAddressLength = 6;
            ulong length = MacAddressLength;
            var macBytes = new byte[MacAddressLength];

            if (SendARP(BitConverter.ToUInt32(ipAddress.GetAddressBytes(), 0), 0, macBytes, ref length) != 0)
            {
                Console.WriteLine("SendARP failed :'(");
            }

            return new PhysicalAddress(macBytes);
        }

        public uint GetServerInfo(String servername)
        {
            IntPtr pSI = IntPtr.Zero;
            SERVER_INFO_100 serverInfo;

            if (NetServerGetInfo(servername, 101, out pSI) != 0)
            {
                Console.WriteLine(String.Format("Couldn't get {0}'s information.", servername));
                return 0;
            }

            serverInfo = (SERVER_INFO_100)Marshal.PtrToStructure(pSI, typeof(SERVER_INFO_100));

            return serverInfo.PlatformId;
        }
    }
}
