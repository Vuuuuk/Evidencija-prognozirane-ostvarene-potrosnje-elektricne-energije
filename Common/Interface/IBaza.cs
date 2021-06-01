using Common.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface
{
    public interface IBaza
    {
        void IzvrsiUpisSvihPodataka();
        void UpisPotrosnje(DateTime vreme, string safeFileName, string lokacija, Potrosnja potrosnja, DateTime datum, string tabela);
        void UpisNevalidnogFajla(DateTime vreme, string safeFileName, string lokacija, int brojRedova);
        List<string> GeoLokacije();
        bool FajlUcitan(string imeFajla);
        void IsprazniBazu();
        List<IPotrosnja> VratiPotrosnju(string ime, string lokacija, string datum);
        void EvidentirajGeoLokaciju(string oblast);
    }
}
