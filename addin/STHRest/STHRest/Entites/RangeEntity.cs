using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STHRest.Models;
using Newtonsoft.Json;

namespace STHRest.Entites
{

    public class RangeEntity : Entity
    {
        string LIST_RANGE_URL = "http://spotfire-server.zepower.com:8080/snip/snip/listranges";

        public List<RangeModel> GetRanges(){

            var rawData = GetData(LIST_RANGE_URL);
            var data = JsonConvert.DeserializeObject<List<RangeModel>>(rawData);

            return data;
        }

    }
}
