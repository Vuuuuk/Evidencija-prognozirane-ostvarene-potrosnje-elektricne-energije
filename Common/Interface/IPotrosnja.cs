using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface
{
    public interface IPotrosnja
    {
        int sat { get; set; }
        int load { get; set; }
        string oblast { get; set; }
    }
}
