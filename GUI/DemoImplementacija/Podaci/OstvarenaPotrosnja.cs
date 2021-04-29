using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DemoImplementacija.Podaci
{
    public class OstvarenaPotrosnja
    {
        private int sat;
        private int load;
        private string oblast;

        public OstvarenaPotrosnja() { }

        public OstvarenaPotrosnja(int sat, int load, string oblast)
        {
            Sat = sat;
            Load = load;
            Oblast = oblast;
        }

        public int Sat { get => sat; set => sat = value; }
        public int Load { get => load; set => load = value; }
        public string Oblast { get => oblast; set => oblast = value; }
    }
}
