using Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class RelativnoOdstupanje : IRelativnoOdstupanje
    {
        public int sat { get; set; }
        public int ostvarenaPotrosnja { get; set; }
        public int prognoziranaPotrosnja { get; set; }
        public double odstupanje { get; set; }

        public RelativnoOdstupanje(int sat, int ostvarenaPotrosnja, int prognoziranaPotrosnja, double odstupanje)
        {
            this.sat = sat;
            this.ostvarenaPotrosnja = ostvarenaPotrosnja;
            this.prognoziranaPotrosnja = prognoziranaPotrosnja;
            this.odstupanje = odstupanje;
        }

        public override string ToString()
        {
            return String.Format("{0},{1},{2},{3:0.000}", sat, ostvarenaPotrosnja, prognoziranaPotrosnja, odstupanje);
        }
    }
}
