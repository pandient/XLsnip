﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using STHRest.Models;

namespace STHRest.Entites
{
    public class RangeDataEntity : Entity
    {
        String UPLOAD_URL = "http://spotfire-server.zepower.com:8080/snip/" + "snip/UploadRange";
        String TARGETED_UPLOAD_URL = "http://spotfire-server.zepower.com:8080/snip/" + "snip/TargetedUploadRange";
        String DOWNLOAD_URL = "http://spotfire-server.zepower.com:8080/snip/" + "snip/DownloadRange";

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


        public string TargetedUploadRange(String UserName, String RangeName, String Description, String Address, String[][] Data, String[] AllowedUsers)
        {
            Dictionary<String, String> data = new Dictionary<string, string>();
            data.Add("UserName", UserName);
            data.Add("RangeName", RangeName);
            data.Add("Description", Description);
            data.Add("Address", Address);
            data.Add("AllowedUsers", String.Join(",", AllowedUsers ));
            data.Add("Data", JsonConvert.SerializeObject(Data));
            String response = PostData(TARGETED_UPLOAD_URL, data);
            return response;
        }

        public RangeDataModel DownloadRange(int RangeId)
        {
            var rawData = GetData(DOWNLOAD_URL + "?RangeId=" + RangeId);
            var data = JsonConvert.DeserializeObject<RangeDataModel>(rawData);

            return data;

        }
    }
}
