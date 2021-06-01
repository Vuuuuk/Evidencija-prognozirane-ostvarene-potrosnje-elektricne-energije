using Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Potrosnja : IPotrosnja
    {
        public int sat { get; set; }
        public int load { get; set; }
        public string oblast { get; set; }

        public Potrosnja() { }

        public Potrosnja(int sat, int load, string oblast)
        {
            this.sat = sat;
            this.load = load;
            this.oblast = oblast;
        }
    }
}
