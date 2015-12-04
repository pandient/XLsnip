using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using STHRest.Entites;
using STHRest.Models;
using System.IO;

 
namespace STHRest
{
    [ComVisible(true)]
    public class CXlService
    {

        public CXlService()
        { 
        }

        //public RangeModel[] GetRangeList()
        //{
        //    RangeEntity entity = new RangeEntity();
        //    List<RangeModel> ranges = entity.GetRanges();

        //    return ranges.ToArray();
        public string GetRangeList()
        {
            RangeEntity entity = new RangeEntity();
            List<RangeModel> result = entity.GetMyRanges(Environment.UserName);

            string fileName = GetTempName();

            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine(result.Count.ToString());
                    sw.WriteLine();

                    for (int r = 0; r < result.Count; r++)
                    {
                        sw.WriteLine(result[r].Id.ToString());
                        sw.WriteLine(result[r].Name);
                        sw.WriteLine(result[r].Desc);
                        sw.WriteLine(result[r].UserName);
                    }

                    sw.Close();
                }
            }

            return fileName;
        }

        public string UploadRange(string name, string desc, string xlAddress, ref string []allowedUsers, string fileName)
        {
            RangeDataEntity entity = new RangeDataEntity();
            string result;
            string[][] data;
            int rows;
            int cols;

            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    rows = int.Parse(sr.ReadLine());
                    cols = int.Parse(sr.ReadLine());
                    sr.ReadLine();

                    data = new string[rows][];
                    for (int r = 0; r < rows; r++)
                    {
                        data[r] = new string[cols];
                        for (int c = 0; c < cols; c++)
                        {
                            data[r][c] = sr.ReadLine();
                        }
                    }
                }
            }

            result = entity.TargetedUploadRange(Environment.UserName, name, desc, xlAddress, data, allowedUsers);
            File.Delete(fileName);

            return result;
        }

        private string GetTempName()
        {
            return Path.GetTempPath() + "STH" + DateTime.Now.ToString("yyyymmdd hhnnss") + ".txt";
        }

        public string DownloadRange(int id)
        {
            string fileName = GetTempName();

            RangeDataEntity entity = new RangeDataEntity();
            RangeDataModel result = entity.DownloadRange(id);

            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine(result.Data.Length.ToString());
                    sw.WriteLine(result.Data[0].Length.ToString());
                    sw.WriteLine();

                    for (int r = 0; r < result.Data.Length; r++)
                    {
                        for (int c = 0; c < result.Data[r].Length; c++)
                        {
                            sw.WriteLine(result.Data[r][c]);
                        }
                    }

                    sw.Close();
                }
            }

            return fileName;
        }
    }

}
