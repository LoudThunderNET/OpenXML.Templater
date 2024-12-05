using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenXML.Xlsx.Templater
{
    public interface IDataContext<TDataModel>
        where TDataModel : class
    {
        void SetContext();
    }
}
