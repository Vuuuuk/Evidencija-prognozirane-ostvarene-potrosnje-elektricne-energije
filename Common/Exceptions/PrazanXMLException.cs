using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class PrazanXMLException : Exception
    {
        public PrazanXMLException(string poruka) : base(poruka) { }
        public PrazanXMLException() : this("XML fajl ne sme biti prazan, greška prilikom upisivanja!") { }
    }
}
