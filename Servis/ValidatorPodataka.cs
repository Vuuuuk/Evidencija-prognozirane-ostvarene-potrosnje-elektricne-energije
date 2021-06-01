using Common.Interface;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servis
{
    public class ValidatorPodataka : IValidatorPodataka
    {
        private static int brojSatiUDanu = 24;

        public bool Validator(List<IPotrosnja> list)
        {
            bool valid = true;
            var oblasti = list.Select(x => x.oblast).Distinct();

            foreach (string ob in oblasti)
            {
                var sati = list.Where(s => s.oblast == ob);
                if (sati.Count() != brojSatiUDanu)
                {
                    Console.WriteLine("[GREŠKA] Nevalidan fajl -> Broj sati u danu za oblast {0} = {1}", ob, sati.Count());
                    valid = false;
                }
            }

            return valid;
        }
    }
}
