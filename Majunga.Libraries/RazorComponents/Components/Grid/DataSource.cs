using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Majunga.Libraries.RazorComponents.Components.Grid
{
    public class DataSource<TModel>
    {
        public Func<object> Create { get; set; }
        public Func<object> Read { get; set; }
        public Func<object> Update { get; set; }
        public Func<object> Delete { get; set; }
    }
}
