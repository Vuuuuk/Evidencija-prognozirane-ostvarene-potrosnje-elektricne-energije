using BazaPodataka;
using Common.Exceptions;
using Common.Interface;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servis
{
    public class Evidentiranje : IEvidentiranje
    {
        private IBaza baza;

        public Evidentiranje(IBaza b)
        {
            this.baza = b;
        }

        public List<string> EvidentirajOblasti(List<IPotrosnja> potrosnja)
        {
            //EXCEPTION
            if (potrosnja.Count.Equals(0))
                throw new PrazanArgumentException();

            List<string> postojeceOblasti = baza.GeoLokacije();
            var noveOblasti = potrosnja.Select(x => x.oblast).Distinct();

            List<string> oblastiZaEvidentiranje = new List<string>();

            foreach (string oblast in noveOblasti)
            {
                if (!postojeceOblasti.Contains(oblast))
                    oblastiZaEvidentiranje.Add(oblast);
            }

            if (oblastiZaEvidentiranje.Count() != 0)
            {
                foreach (string oblast in oblastiZaEvidentiranje)
                    baza.EvidentirajGeoLokaciju(oblast);
            }

            return oblastiZaEvidentiranje;
        }
    }
}
