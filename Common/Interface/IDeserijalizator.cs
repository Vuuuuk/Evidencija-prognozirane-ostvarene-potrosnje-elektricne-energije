using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface
{
    public interface IDeserijalizator
    {
        void LoadXMLOstvarena(MemoryStream ms);

        void LoadXMLPrognozirana(MemoryStream ms);

        void ParsiranjeXMLOstvarena();

        void ParsiranjeXMLPrognozirana();

        DateTime ParseDatum(string filename);
    }
}
