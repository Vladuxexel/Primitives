using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    /// <summary>
    /// View model class for representing information in datagrid
    /// </summary>
    public class ViewPropsVM 
    {
        private readonly Action<double> _setter;
        private readonly Func<double> _getter;

        /// <summary>
        /// Displaying properties on datadrid
        /// </summary>
        /// <param name="name"></param>
        /// <param name="setter"></param>
        /// <param name="getter"></param>
        public ViewPropsVM(string name, Action<double> setter , Func<double> getter)
        {
            _setter = setter;
            _getter = getter;
            Name = name;
        }

        public string Name { get; set; }
        
        public double Value { get { return _getter();} set { _setter(value); }}
    }
}
