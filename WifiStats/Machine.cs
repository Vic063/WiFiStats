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
        private IPAddress ip;
        private String mac;

        [DataMember]
        public IPAddress IP
        {
            get
            {
                return this.ip;
            }

            set
            {
                this.ip = value;
            }
        }

        [DataMember]
        public string MAC
        {
            get
            {
                return mac;
            }
            set
            {
                mac = value;
            }
        }

        [DataMember]
        public string OS
        {
            get
            {
                return "Inconnu";
            }
            set
            {
            }
        }

        [DataMember]
        public string Constructeur
        {
            get
            {
                return "Inconnu";
            }
            set
            {
            }
        }

        [DataMember]
        public TypeMachine TypeMachine
        {
            get
            {
                return TypeMachine.Desktop;
            }
            set
            {
            }
        }

        [DataMember]
        public string HostName
        {
            get;
            set;
        }
    }
}
