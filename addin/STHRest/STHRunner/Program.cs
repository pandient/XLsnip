using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STHRest.Entites;

namespace STHRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            //var entity = new RangeEntity();
            //var data = entity.GetRanges();

            //var data2 = new string[2][];
            //data2[0] = new String[] { "1", "2"};
            //data2[1] = new String[] { "3", "4"};


            //var entity2 = new RangeDataEntity();
            //entity2.UploadRange("Guest", "435test", "foobar", "$A1:$B2", data2);


            //var entity3 = new RangeDataEntity();
            //var result = entity2.DownLoadRange(4);

            var entity = new RangeEntity();
            var data = entity.GetMyRanges("tony.lei");

            var data2 = new string[2][];
            data2[0] = new String[] { "1", "2" };
            data2[1] = new String[] { "3", "4" };


            var entity2 = new RangeDataEntity();
            entity2.TargetedUploadRange("Guest", "435test", "foobar", "$A1:$B2", data2, new String[] { "saad.shaharyar,tony.lei"});


        }
    }
}
