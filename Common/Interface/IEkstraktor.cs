using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Common.Interface
{
    public interface IEkstraktor
    {
        string CuvanjePodatakaCSV(DataGrid podaci);
    }
}
