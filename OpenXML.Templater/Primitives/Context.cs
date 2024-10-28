using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenXML.Templater.Primitives
{
    internal struct Context
    {
        private object _obj;
        public Context(object obj)
        {
            _obj = (obj == null) ? new object() : obj;
            var objectMap = new Dictionary<string, object?>();
            var properties = _obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                //if(property.PropertyType)
                objectMap[property.Name] = property.GetValue(_obj);
            }
        }
    }
}
