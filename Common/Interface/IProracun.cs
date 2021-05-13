using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface
{
    public interface IProracun
    {
        List<RelativnoOdstupanje> IzracunajOdstupanje(List<Potrosnja> ostvarena, List<Potrosnja> prognozirana);
        List<Potrosnja> PopuniListuPotrosnje(string ime, string lokacija, string datum);
    }
}
