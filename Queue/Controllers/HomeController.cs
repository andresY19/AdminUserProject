using Queue.App_Start;
using Queue.DAL;
using Queue.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace Queue.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private QueueContext db = new QueueContext();

        [SessionAuthorize]
        public ActionResult Index()
        {
            Guid  company = Guid.Parse(Request.RequestContext.HttpContext.Session["Company"].ToString());
            ViewBag.empleados = db.Agent_Employee.Where(f => f.IdCompany == company).OrderBy(o => o.Nombre).ToList();
            return View();
        }

        [SessionAuthorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [SessionAuthorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

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