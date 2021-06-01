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
    public class ValidatorPodataka : IValidatorPodataka
    {
        public bool Validator(DateTime datum, List<IPotrosnja> list)
        {
            //EXCEPTION
            if(datum == DateTime.MinValue || list.Count.Equals(0))
            {
                throw new PrazanArgumentException();
            }
            int brojSatiUDanu = 24;
            var dst = TimeZone.CurrentTimeZone.GetDaylightChanges(datum.Year);
            if (dst.End > datum && dst.End < datum.AddDays(1))
                brojSatiUDanu = 23;
            if (dst.Start > datum && dst.Start < datum.AddDays(1))
                brojSatiUDanu = 25;

            bool valid = true;
            var oblasti = list.Select(x => x.oblast).Distinct();

            foreach (string ob in oblasti)
            {
                var sati = list.Where(s => s.oblast == ob);
                if (sati.Count() != brojSatiUDanu)
                {
                    Console.WriteLine("[GREŠKA] Nevalidan fajl -> Broj sati u danu za oblast {0} = {1}", ob, sati.Count());
                    Console.WriteLine("Datum {0} ima {1} sati.", datum.ToShortDateString(), brojSatiUDanu);
                    valid = false;
                }
            }

            return valid;
        }
    }
}
