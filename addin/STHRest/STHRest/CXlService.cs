using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace STHRest
{
    [ComVisible(true)]
    public class CXlService
    {

        public CXlService()
        {
 
        }

        public string ReturnString(string a)
        {
            return "Test Successful" + a;
        }

        

        public string GetRanges()
        {


            return String.Empty;
        }
 

    }
}
