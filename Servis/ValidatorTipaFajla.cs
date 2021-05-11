using Common.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servis
{
    public class ValidatorTipaFajla : IValidatorTipaFajla
    {
        public bool ValidatorTipa(string imeFajla)
        {
            bool valid = true;
            if (!String.Equals(Path.GetExtension(imeFajla), ".xml", StringComparison.OrdinalIgnoreCase))
                valid = false;
            return valid;
        }

        // Jos uvek ne koristimo ovu metodu
        // TODO : Prognozirana / ostvarena potrosnja se moze dodati samo u polje predvidjeno za prog. / ostv. potrosnju
        public bool ValidatorImena(string imeFajla, string template)
        {
            bool valid = true;
            if (!imeFajla.Contains(template))
                valid = false;
            return valid;
        }
    }
}
