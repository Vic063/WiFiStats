﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace WifiStats
{
    public class Machine
    {
        private IPAddress ip;

        public System.Net.IPAddress IP
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

        public string MAC
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public string OS
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public string Constructeur
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public TypeMachine TypeMachine
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public string HostName
        {
            get;
            set;
        }
    }
}
