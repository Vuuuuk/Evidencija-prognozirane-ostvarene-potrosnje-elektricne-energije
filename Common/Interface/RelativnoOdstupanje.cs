using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface
{
    public interface IRelativnoOdstupanje
    {
        int sat { get; set; }
        int ostvarenaPotrosnja { get; set; }
        int prognoziranaPotrosnja { get; set; }
        double odstupanje { get; set; }
    }
}
