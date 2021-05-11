using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Potrosnja
    {
        private int sat;
        private int load;
        private string oblast;

        public Potrosnja() { }

        public Potrosnja(int sat, int load, string oblast)
        {
            this.sat = sat;
            this.load = load;
            this.oblast = oblast;
        }

        public int Sat
        {
            get { return sat; }
            set { sat = value; }
        }

        public int Load
        {
            get { return load; }
            set { load = value; }
        }

        public string Oblast
        {
            get { return oblast; }
            set { oblast = value; }
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", Sat, Load, Oblast);
        }
    }
}
