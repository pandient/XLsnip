using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace STHRest.Models
{
    [ComVisible(true), ClassInterface(ClassInterfaceType.AutoDual)]
    public class RangeDataModel
    {
        public String Address { get; set; }
        public String[][] Data { get; set; }

    }
}
