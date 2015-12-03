using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace STHRest.Models
{
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual)]
    public class RangeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
		public string UserName { get; set; } 
    }
}
