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
    public class Proracun : IProracun
    {
        private IBaza baza;
        public Proracun(IBaza b)
        {
            this.baza = b;
        }

        public List<IRelativnoOdstupanje> IzracunajOdstupanje(List<IPotrosnja> ostvarena, List<IPotrosnja> prognozirana)
        {
            //EXCEPTION
            if(ostvarena.Count.Equals(0) || prognozirana.Count.Equals(0))
            {
                throw new PrazanArgumentException();
            }

            List<IRelativnoOdstupanje> lista = new List<IRelativnoOdstupanje>();

            for (int i = 0; i < ostvarena.Count; i++)
            {
                for (int j = 0; j < prognozirana.Count; j++)
                {
                    if(ostvarena[i].sat == prognozirana[j].sat)
                    {
                        double odstupanje = Math.Round(Math.Abs((double)ostvarena[i].load - prognozirana[j].load) / ostvarena[i].load * 100 , 3);
                        lista.Add(new RelativnoOdstupanje(ostvarena[i].sat, ostvarena[i].load, prognozirana[j].load, odstupanje));
                    }
                }
            }

            return lista;
        }

        public List<IPotrosnja> PopuniListuPotrosnje(string ime, string lokacija, string datum)
        {
            // EXCEPTION
            if(ime.Equals(String.Empty) || lokacija.Equals(String.Empty) || datum.Equals(String.Empty))
            {
                throw new PrazanArgumentException();
            }
            return baza.VratiPotrosnju(ime, lokacija, datum);
        }
    }

}
