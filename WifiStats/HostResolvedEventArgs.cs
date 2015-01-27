using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WifiStats
{
    public class HostResolvedEventArgs : EventArgs
    {

        private List<Machine> listMachines;

        public List<Machine> Machines
        {
            get { return listMachines; }
        }

        public HostResolvedEventArgs(List<Machine> listMachines)
        {
            // TODO: Complete member initialization
            this.listMachines = listMachines;
        }
    }
}
