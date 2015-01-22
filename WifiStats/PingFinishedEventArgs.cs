using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WifiStats
{
    public class PingFinishedEventArgs
    {

        private List<Machine> listMachines;

        public List<Machine> Machines
        {
            get { return listMachines; }
        }

        public PingFinishedEventArgs(List<Machine> listMachines)
        {
            // TODO: Complete member initialization
            this.listMachines = listMachines;
        }

    }
}
