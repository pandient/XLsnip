using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XLSnipServer.Models;


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
        public JsonResult UploadRange(String UserName , String Description , String Data)
        {

            return null;
        }


        public JsonResult DownloadRange(int RangeId)
        {
            return null;
        }


        public ActionResult ListRanges()
        {
            List<RangeModel> ranges = new List<RangeModel>() ;
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
