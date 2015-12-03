using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using STHRest.Entites;
using STHRest.Models;

 
namespace STHRest
{
    [ComVisible(true)]
    public class CXlService
    {

        public CXlService()
        { 
        }

        public RangeModel[] GetRangeList()
        {
            RangeEntity entity = new RangeEntity();
            List<RangeModel> ranges = entity.GetRanges();

            return ranges.ToArray();
        }

        public string UploadRange(string user, string name, string desc, string xlAddress, string fileName)
        {
            RangeDataEntity entity = new RangeDataEntity();

            var data2 = new string[2][];
            data2[0] = new String[] { "1", "2" };
            data2[1] = new String[] { "3", "4" };

            string result;
            
            result = entity.UploadRange("Guest", "TestRange", "foobar", "$A1:$B2", data2);
            return result;
        }

        public RangeDataModel DownloadRange(int id)
        {
            RangeDataEntity entity = new RangeDataEntity();
            RangeDataModel result = entity.DownLoadRange(id);

            return result;
        }
    }

}
