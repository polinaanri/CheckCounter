using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckCounterMain
{
    public class Consumer
    {
        private string name;
        private bool checkResult = true;

        private IList<Building>? buildings = new List<Building>();

        public IList<Building> Buildings { get => buildings; set => buildings = value; }
        public string Name { get => name; set => name = value; }
        public bool CheckResult { get => checkResult; set => checkResult = value; }
    }
}
