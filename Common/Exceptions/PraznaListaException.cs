using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class PraznaListaException : Exception
    {
        public PraznaListaException(string poruka) : base(poruka) { }
        public PraznaListaException() : this("Lista ne sme biti prazna!") { }
    }
}
