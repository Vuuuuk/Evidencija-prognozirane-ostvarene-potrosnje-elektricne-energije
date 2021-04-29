using GUI.DemoImplementacija.Podaci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DemoImplementacija
{
    public class Validator
    {

        public const int validanBrojSatiUDanu = 300;

        public bool ValidacijaPodatakaOstvarena(List<OstvarenaPotrosnja> op)
        {
            bool povratna = true;

            List<string> podrucja = new List<string>();

            int brojSati = 0;

            foreach(OstvarenaPotrosnja stavka in op)
            {
                if (!podrucja.Contains(stavka.Oblast))
                    podrucja.Add(stavka.Oblast);
                brojSati += stavka.Sat;
            }

            if ((validanBrojSatiUDanu * podrucja.Count()) != brojSati)
                povratna = false;

            return povratna;
        }

        public bool ValidacijaPodatakaPrognozirana(List<PrognoziranaPotrosnja> op)
        {
            bool povratna = true;

            List<string> podrucja = new List<string>();

            int brojSati = 0;

            foreach (PrognoziranaPotrosnja stavka in op)
            {
                if (!podrucja.Contains(stavka.Oblast))
                    podrucja.Add(stavka.Oblast);
                brojSati += stavka.Sat;
            }

            if ((validanBrojSatiUDanu * podrucja.Count()) != brojSati)
                povratna = false;

            return povratna;
        }
    }
}
