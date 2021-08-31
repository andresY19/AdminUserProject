using Queue.DAL;
using Queue.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Queue.Controllers
{
    public class ReportApplicationsController : Controller
    {
        private QueueContext db = new QueueContext();
        // GET: ReportApplications
        public ActionResult Index()
        {
            ViewBag.DateFrom = DateTime.Now.AddDays(-2);
            ViewBag.DateTo = DateTime.Now;
            return View();
        }

        public JsonResult NewChart(string dateFrom, string dateTo)
        {
            var company = Request.RequestContext.HttpContext.Session["Company"].ToString();
            OperationController opc = new OperationController();
            DateTime fromdate = Convert.ToDateTime(dateFrom);
            DateTime todate = Convert.ToDateTime(dateTo);
            BasicStatsModel bm = opc.MoreUsedApp(company, fromdate, todate);

            List<object> iData = new List<object>();

            //Creating sample data  
            DataTable dt = new DataTable();

            dt.Columns.Add("Aplicaciones", System.Type.GetType("System.String"));
            dt.Columns.Add("Tiempo", System.Type.GetType("System.Int32"));

            if (bm.labels.Count > 10)
            {
                for (var i = 0; i < 9; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["Aplicaciones"] = bm.labels[i].ToString();
                    dr["Tiempo"] = bm.data[i];
                    dt.Rows.Add(dr);
                }
            }
            else
            {
                for (var i = 0; i < bm.labels.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["Aplicaciones"] = bm.labels[i].ToString();
                    dr["Tiempo"] = bm.data[i];
                    dt.Rows.Add(dr);
                }
            }    
            

            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            //Source data returned as JSON  
            return Json(iData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GaugeChart()
        {
            var company = Request.RequestContext.HttpContext.Session["Company"].ToString();
            OperationController opc = new OperationController();
            BasicStatsModel bm = opc.TypeApp(company);

            Guid idcompany = Guid.Parse(Request.RequestContext.HttpContext.Session["Company"].ToString());
            var programs = db.Agent_ProgramClasification.Where(t => t.Agent_Empresa.IdCompany == idcompany).ToList();

            Agent_ProgramClasification p = new Agent_ProgramClasification();

            TimesUser entities = new TimesUser();

                for (var i = 0; i < bm.labels.Count; i++)
                {
                foreach (var n in programs)
                {
                    p.name = n.name;
                    p.clasification = n.clasification;

                    if (bm.labels[i] == p.name)
                    {
                        switch (p.clasification)
                        {
                            case 1:
                                entities.Productivo = entities.Productivo + bm.data[i];
                                break;
                            case 2:
                                entities.Improductivo = entities.Improductivo + bm.data[i];
                                break;
                            case 3:
                                entities.Neutro = entities.Neutro + bm.data[i];
                                break;
                            default:
                                // code block
                                break;
                        }
                    }

                }
            }


            return Json(entities, JsonRequestBehavior.AllowGet);
        }
        // GET: ReportApplications/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: ReportApplications/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: ReportApplications/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ReportApplications/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: ReportApplications/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ReportApplications/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: ReportApplications/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
