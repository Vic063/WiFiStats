using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WifiStats
{
    [DataContract]
    public class Reseau
    {
        [DataMember]
        public System.Net.IPAddress IP
        {
            get;
            set;
        }

        [DataMember]
        public string SSID
        {
            get;
            set;
        }
        [DataMember]
        public List<Machine> Machines
        {
            get;
            set;
        }
    }
}
