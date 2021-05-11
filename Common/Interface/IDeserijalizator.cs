using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface
{
    public interface IDeserijalizator
    {
        void LoadXMLOstvarena(OpenFileDialog ofdOstvarena);

        void LoadXMLPrognozirana(OpenFileDialog ofdPrognozirana);

        void ParsiranjeXMLOstvarena();

        void ParsiranjeXMLPrognozirana();

        DateTime ParseDatum(string filename);
    }
}
