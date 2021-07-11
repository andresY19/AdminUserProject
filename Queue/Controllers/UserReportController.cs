using Queue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Queue.Controllers
{
    public class UserReportController : Controller
    {
        // GET: UserReport
        public ActionResult Index()
        {
            return View();
        }



        public JsonResult MoreUsedApp()
        {
            var company = Request.RequestContext.HttpContext.Session["Company"].ToString();
            OperationController opc = new OperationController();
            DateTime fromdate = DateTime.Today.AddDays(-10);
            DateTime todate = DateTime.Today;
            BasicStatsModel bm = opc.MoreUsedApp(company, fromdate, todate);
            return Json(bm, JsonRequestBehavior.AllowGet);
        }


    }
}