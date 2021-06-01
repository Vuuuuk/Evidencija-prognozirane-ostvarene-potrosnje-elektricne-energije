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

        public override bool Equals(object obj)
        {
            Potrosnja p = obj as Potrosnja;
            if (p.sat == sat && p.load == load && p.oblast == oblast)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
