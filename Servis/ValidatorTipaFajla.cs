using Common.Exceptions;
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
            // EXCEPTION
            if(imeFajla.Equals(String.Empty))
            {
                throw new PrazanArgumentException();
            }    
            bool valid = true;
            if (!String.Equals(Path.GetExtension(imeFajla), ".xml", StringComparison.OrdinalIgnoreCase))
                valid = false;
            return valid;
        }
    }
}
