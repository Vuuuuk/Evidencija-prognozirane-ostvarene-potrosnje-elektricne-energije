using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class RelativnoOdstupanje
    {
        private int sat;
        private int ostvarenaPotrosnja;
        private int prognoziranaPotrosnja;
        private double odstupanje;

        public RelativnoOdstupanje(int sat, int ostvarenaPotrosnja, int prognoziranaPotrosnja, double odstupanje)
        {
            this.sat = sat;
            this.ostvarenaPotrosnja = ostvarenaPotrosnja;
            this.prognoziranaPotrosnja = prognoziranaPotrosnja;
            this.odstupanje = odstupanje;
        }

        public int Sat
        {
            get { return sat; }
            set { sat = value; }
        }

        public int OstvarenaPotrosnja
        {
            get { return ostvarenaPotrosnja; }
            set { ostvarenaPotrosnja = value; }
        }

        public int PrognoziranaPotrosnja
        {
            get { return prognoziranaPotrosnja; }
            set { prognoziranaPotrosnja = value; }
        }

        public double Odstupanje
        {
            get { return odstupanje; }
            set { odstupanje = value; }
        }

        public override string ToString()
        {
            return String.Format("{0},{1},{2},{3:0.000}", Sat, OstvarenaPotrosnja, PrognoziranaPotrosnja, Odstupanje);
        }
    }
}
