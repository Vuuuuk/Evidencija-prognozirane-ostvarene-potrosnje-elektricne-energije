using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface
{
    public interface IValidatorTipaFajla
    {
        bool ValidatorTipa(string imeFajla);

        bool ValidatorImena(string imeFajla, string template);
    }
}
