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
        List<IRelativnoOdstupanje> IzracunajOdstupanje(List<IPotrosnja> ostvarena, List<IPotrosnja> prognozirana);
        List<IPotrosnja> PopuniListuPotrosnje(string ime, string lokacija, string datum);
    }
}
