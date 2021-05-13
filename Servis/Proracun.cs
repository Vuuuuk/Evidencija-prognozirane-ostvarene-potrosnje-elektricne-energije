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
    public class Proracun : IProracun
    {
        Baza baza = new Baza();
        public List<RelativnoOdstupanje> IzracunajOdstupanje(List<Potrosnja> ostvarena, List<Potrosnja> prognozirana)
        {
            List<RelativnoOdstupanje> lista = new List<RelativnoOdstupanje>();

            for (int i = 0; i < ostvarena.Count; i++)
            {
                for (int j = 0; j < prognozirana.Count; j++)
                {
                    if(ostvarena[i].Sat == prognozirana[j].Sat)
                    {
                        double odstupanje = Math.Round(Math.Abs((double)ostvarena[i].Load - prognozirana[j].Load) / ostvarena[i].Load * 100 , 3);
                        lista.Add(new RelativnoOdstupanje(ostvarena[i].Sat, ostvarena[i].Load, prognozirana[j].Load, odstupanje));
                    }
                }
            }

            return lista;
        }

        public List<Potrosnja> PopuniListuPotrosnje(string ime, string lokacija, string datum)
        {
            return baza.VratiPotrosnju(ime, lokacija, datum);
        }
    }

}
