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
                var qryUser = context.Users.Where(x => x.Name.Equals(UserName));
                User userEntity= qryUser.FirstOrDefault();
                if (userEntity == null)
                {
                    userEntity = new User() { Name = UserName };
                    context.Users.Add(userEntity);
                    context.SaveChanges();
                }
                var qryUserRange = context.UserRanges.Where(x => x.RangeName.Equals(RangeName) && x.UserId.Equals(userEntity.Id));
                UserRange UserRangeEntity = qryUserRange.FirstOrDefault();
                if (UserRangeEntity == null)
                {
                    UserRange range = new UserRange();
                    range.Description = Description;
                    range.RangeName = RangeName;
                    range.UserId = userEntity.Id;
                    context.UserRanges.Add(range);
                    context.SaveChanges();

                    RangeData data = new RangeData();
                    data.Address = Address;
                    data.Data = Data;
                    data.RangeId = range.Id;
                    context.RangeDatas.Add(data);
                    context.SaveChanges();
                }
                else
                {
                    //update
                    UserRangeEntity.RangeName = RangeName;
                    UserRangeEntity.Description = Description;
                    context.SaveChanges();

                    var qryRangeData = context.RangeDatas.Where(x => x.RangeId.Equals(UserRangeEntity.Id));
                    RangeData rData = qryRangeData.FirstOrDefault();
                    if (rData == null)
                    {
                        rData = new RangeData();
                        rData.Address = Address;
                        rData.Data = Data;
                        rData.RangeId = userEntity.Id;
                        context.RangeDatas.Add(rData);
                    }
                    else
                    {
                        rData.Data = Data;
                        rData.Address = Address;
                    }
                    context.SaveChanges();
                }
                
            }
            return Json("Success", JsonRequestBehavior.AllowGet) ;
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
            using (var context = new xlsnippingtoolEntities())
            {
                var qry = context.UserRanges.ToList();
                var usersList = context.Users.ToList();
                foreach ( var item in qry)
                {
                    var qryUser = usersList.Where(x => x.Id.Equals(item.UserId));
                    User userEntity = qryUser.FirstOrDefault();
                    string userName = userEntity != null ? userEntity.Name : "unknown";
                    ranges.Add(new RangeModel() { Id = item.Id, Name = item.RangeName, Desc = item.Description, UserName = userName});
                }
            }
            return Json(ranges, JsonRequestBehavior.AllowGet);
        }
    }
}
