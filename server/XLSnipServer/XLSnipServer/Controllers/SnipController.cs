using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public JsonResult UploadRange()
        {

            return null;
        }


        public JsonResult DownloadRange()
        {
            return null;
        }


        public ActionResult ListRanges()
        {
            List<UserRange> ranges = null;
            using (var context = new xlsnippingtoolEntities()){
                ranges = context.UserRanges.ToList();
                return Json(ranges, JsonRequestBehavior.AllowGet);
            }


            return Json("list", JsonRequestBehavior.AllowGet);
        }
    }
}
