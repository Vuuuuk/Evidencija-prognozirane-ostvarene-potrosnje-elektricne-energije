using BazaPodataka;
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
        Baza baza = new Baza();
        public void EvidentirajOblasti(List<Potrosnja> potrosnja)
        {
            List<string> postojeceOblasti = baza.GeoLokacije();
            var noveOblasti = potrosnja.Select(x => x.Oblast).Distinct();

            List<string> oblastiZaEvidentiranje = new List<string>();

            foreach (string oblast in noveOblasti)
            {
                if (!postojeceOblasti.Contains(oblast))
                    oblastiZaEvidentiranje.Add(oblast);
            }

            if (oblastiZaEvidentiranje.Count != 0)
            {
                foreach (string oblast in oblastiZaEvidentiranje)
                {
                    baza.EvidentirajGeoLokaciju(oblast);
                }
            }
        }
    }
}
