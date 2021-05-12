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
        void UpisPotrosnje(DateTime vreme, OpenFileDialog file, Potrosnja potrosnja, DateTime datum, string tabela);
        void UpisNevalidnogFajla(DateTime vreme, OpenFileDialog file, int brojRedova);
        List<string> GeoLokacije();
        List<RelativnoOdstupanje> ProracunOdstupanja(string lokacija, string datum);
        bool FajlUcitan(string imeFajla);
    }
}
