using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class PrazanArgumentException : Exception
    {
        public PrazanArgumentException(string poruka) : base(poruka) { }
        public PrazanArgumentException() : this("Argument ne sme biti prazan, greška u prenosu fajla!") { }
    }
}
