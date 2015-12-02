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
            var entity = new RangeEntity();
            var data = entity.GetRanges();
        }
    }
}
