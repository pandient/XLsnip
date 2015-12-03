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

        public RangeModel[] GetRangeList()
        {
            RangeEntity entity = new RangeEntity();
            List<RangeModel> ranges = entity.GetRanges();

            return ranges.ToArray();
        }

        public string UploadRange(string user, string name, string desc, string xlAddress, string fileName)
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

            result = entity.UploadRange(user, name, desc, xlAddress, data);
            return result;
        }

        public string DownloadRange(int id)
        {
            string fileName = Path.GetTempPath() + "STH" + DateTime.Now.ToString("yyyymmdd hhnnss") + ".txt";

            RangeDataEntity entity = new RangeDataEntity();
            RangeDataModel result = entity.DownLoadRange(id);

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
