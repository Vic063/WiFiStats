using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WifiStats
{
    public class Reseau
    {
        public System.Net.IPAddress IP
        {
            get
            {
                return this.IP;
            }
            set
            {
                this.IP = value;
            }
        }

        public string SSID
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public List<Machine> Machines
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
