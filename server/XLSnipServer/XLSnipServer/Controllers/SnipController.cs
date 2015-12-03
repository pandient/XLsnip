using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XLSnipServer.Models;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


namespace XLSnipServer.Controllers
{
    public class SnipController : Controller
    {
        //
        // GET: /Snip/

        public ActionResult Index()
        {
            return Content("asdf") ;
        }

        [HttpPost]
        public JsonResult UploadRange(String UserName , String RangeName ,String Description , String Address,String Data )
        {
            using (var context = new xlsnippingtoolEntities())
            {
               

                UserRange range = new UserRange();
                range.Description = Description;
                range.RangeName = RangeName;
                range.UserId = 1;
                context.UserRanges.Add(range);
                context.SaveChanges();
               
                RangeData data = new RangeData();
                data.Address = Address;
                data.Data = Data;
                data.RangeId = range.Id ;
                context.RangeDatas.Add(data);
                context.SaveChanges();
            }

            return Json("Sucess", JsonRequestBehavior.AllowGet) ;
        }


        public JsonResult DownloadRange(int RangeId)
        {
            RangeDataModel rDataModel = new RangeDataModel();
            using (var context = new xlsnippingtoolEntities())
            {
                var qry = context.RangeDatas.ToList().Where(x => x.RangeId.Equals(RangeId));
                RangeData data = qry.FirstOrDefault();
                if (data == null)
                {
                    return null;
                }
                String[][] rData = JsonConvert.DeserializeObject<String[][]>(data.Data);
                rDataModel.Address = data.Address;
                rDataModel.Data = rData;
            }
            return Json(rDataModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteRange(int RangeId)
        {
            using (var context = new xlsnippingtoolEntities())
            {
                foreach (var item in context.RangeDatas)
                {
                    if (item.RangeId == RangeId)
                    {
                        context.RangeDatas.Remove(item);
                    }
                }
                foreach (var item in context.UserRanges) 
                {
                    if (item.Id == RangeId)
                    {
                        context.UserRanges.Remove(item);
                    }
                }
                bool saveFailed;
                var timer = Stopwatch.StartNew();
                do
                {
                    saveFailed = false;
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        if (timer.ElapsedMilliseconds < 60000) saveFailed = true;

                        foreach (var item in ex.Entries)
                        {
                            if (item.State == EntityState.Deleted)
                            {
                                item.State = EntityState.Detached;
                            }
                            else
                            {
                                item.OriginalValues.SetValues(item.GetDatabaseValues());
                            }
                        }
                    }

                } while (saveFailed);
                timer.Stop();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListRanges()
        {
            List<RangeModel> ranges = new List<RangeModel>();
            using (var context = new xlsnippingtoolEntities()){
                var qry = context.UserRanges.ToList();

                foreach ( var item in qry){

                    ranges.Add(new RangeModel() { Id = item.Id, Name = item.RangeName, Desc = item.Description});
                }

                
            }


            return Json(ranges, JsonRequestBehavior.AllowGet);
        }
    }
}
