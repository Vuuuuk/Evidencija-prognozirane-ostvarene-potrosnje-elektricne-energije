using BazaPodataka;
using Common.Interface;
using Common.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servis
{
    public class PristupPodacima : IBaza
    { 
        Baza baza = new Baza();
        Connection connection = new Connection();

        public void ZatvaranjeKonekcije()
        {
            connection.ZatvoriKonekciju();
        }

        public bool ProveraKonekcije()
        {
            return connection.ProveriKonekciju();
        }

        public void IzvrsiUpisSvihPodataka()
        {
            baza.IzvrsiUpisSvihPodataka();
        }

        public void UpisPotrosnje(DateTime vreme, string safeFileName, string lokacija, Potrosnja potrosnja, DateTime datum, string tabela)
        {
            baza.UpisPotrosnje(vreme, safeFileName, lokacija, potrosnja, datum, tabela);
        }

        public void UpisNevalidnogFajla(DateTime vreme, string safeFileName, string lokacija, int brojRedova)
        {
            baza.UpisNevalidnogFajla(vreme, safeFileName, lokacija, brojRedova);
        }

        public List<string> GeoLokacije()
        {
            return baza.GeoLokacije();
        }

        public bool FajlUcitan(string imeFajla)
        {
            return baza.FajlUcitan(imeFajla);
        }

        public void IsprazniBazu()
        {
            baza.IsprazniBazu();
        }

        public List<Potrosnja> VratiPotrosnju(string ime, string lokacija, string datum)
        {
            return baza.VratiPotrosnju(ime, lokacija, datum);
        }
    }
}
