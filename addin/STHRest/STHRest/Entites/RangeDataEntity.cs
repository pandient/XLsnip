using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using STHRest.Models;

namespace STHRest.Entites
{
    public class RangeDataEntity : Entity
    {
        String UPLOAD_URL = "http://localhost:57555/snip/UploadRange";
        String DOWNLOAD_URL = "http://localhost:57555/snip/DownloadRange";

        public string UploadRange(String UserName, String RangeName, String Description, String Address, String[][] Data)
        {
            Dictionary<String, String> data = new Dictionary<string, string>();
            data.Add("UserName", UserName);
            data.Add("RangeName", RangeName);
            data.Add("Description", Description);
            data.Add("Address", Address);
            data.Add("Data", JsonConvert.SerializeObject(Data));
            String response = PostData(UPLOAD_URL, data);
            return response;
        }

        public RangeDataModel DownLoadRange(int RangeId)
        {
            var rawData = GetData(DOWNLOAD_URL + "?RangeId=" + RangeId);
            var data = JsonConvert.DeserializeObject<RangeDataModel>(rawData);

            return data;

        }
    }
}
