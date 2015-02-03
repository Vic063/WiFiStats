using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization;

namespace WifiStats
{
    [DataContract]
    public class Machine
    {
        [DataMember]
        public System.Net.IPAddress IP
        {
            get;
            set;
        }

        [DataMember]
        public string MAC
        {
            get;
            private set;
        }

        [DataMember]
        public string OS
        {
            get;
            private set;
        }

        [DataMember]
        public string Constructeur
        {
            get;
            private set;
        }

        [DataMember]
        public TypeMachine TypeMachine
        {
            get;
            private set;
        }

        [DataMember]
        public string HostName
        {
            get;
            set;
        }
    }
}
