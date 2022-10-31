using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckCounterMain
{
    public class Contract
    {
        private Building? building = new Building();
        
        private string counterSystem = String.Empty;
        private string address = String.Empty;
        private IList<ContractVolume> contractVolumes = new List<ContractVolume>();
        public static List<string> CounterSystems = new List<string>{
            "ТЭ",
            "ГВС"};

        public Building Building { get => building; set => building = value; }
        public string CounterSystem { get => counterSystem; set => counterSystem = value; }
        public string Address { get => address; set => address = value; }
        public IList<ContractVolume> ContractVolumes { get => contractVolumes; set => contractVolumes = value; }

    }
}
