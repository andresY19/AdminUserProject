﻿using Queue.DAL;
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
            Guid idcompany = Guid.Parse(Request.RequestContext.HttpContext.Session["Company"].ToString());
            ViewBag.Department = db.Agent_CompanyDepartment.Where(x => x.IdCompany == idcompany).Distinct().ToList();
           
            return View();
        }

        public JsonResult GetUser(string idDeparment)
        {
            var company = Request.RequestContext.HttpContext.Session["Company"].ToString();
            OperationController opc = new OperationController();
            var result = db.Agent_Employee.Where(d => d.Agent_CompanyDepartment.Id.ToString() == idDeparment).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult NewChart(string dateFrom, string dateTo, string idDeparment)
        {
            var company = Request.RequestContext.HttpContext.Session["Company"].ToString();
            OperationController opc = new OperationController();
            DateTime fromdate = Convert.ToDateTime(dateFrom);
            DateTime todate = Convert.ToDateTime(dateTo);
            BasicUserModel user = opc.GetUsers(company);
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

        public JsonResult ChartWeb(string dateFrom, string dateTo)
        {
            var company = Request.RequestContext.HttpContext.Session["Company"].ToString();
            OperationController opc = new OperationController();
            DateTime fromdate = Convert.ToDateTime(dateFrom);
            DateTime todate = Convert.ToDateTime(dateTo);
            BasicStatsModel bm = opc.WebUsedApp(company, fromdate, todate);

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
                    var str = bm.labels[i].ToString();
                    dr["Aplicaciones"] = str.Substring(0, 15);
                    dr["Tiempo"] = bm.data[i];
                    dt.Rows.Add(dr);
                }
            }
            else
            {
                for (var i = 0; i < bm.labels.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    var str = bm.labels[i].ToString();
                    dr["Aplicaciones"] = str.Substring(0, 15);
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
    
        public JsonResult ChartImproductive(string dateFrom, string dateTo)
        {
            var company = Request.RequestContext.HttpContext.Session["Company"].ToString();
            OperationController opc = new OperationController();
            DateTime fromdate = Convert.ToDateTime(dateFrom);
            DateTime todate = Convert.ToDateTime(dateTo);
            BasicStatsModel bm = opc.ImproductiveUsedApp(company, fromdate, todate);

            List<object> iData = new List<object>();

            var programs = db.Agent_ProgramClasification.Where(t => t.Agent_Empresa.IdCompany.ToString() == company).ToList();

            Agent_ProgramClasification p = new Agent_ProgramClasification();

            TimesUser entities = new TimesUser();

            DataTable dt = new DataTable();

            dt.Columns.Add("Aplicaciones", System.Type.GetType("System.String"));
            dt.Columns.Add("Tiempo", System.Type.GetType("System.Int32"));

            for (var i = 0; i < bm.labels.Count; i++)
            {
                foreach (var n in programs)
                {
                    p.name = n.name;
                    p.clasification = n.clasification;

                    if (bm.labels[i] == p.name)
                    {
                        if(p.clasification == 2)
                        {
                            //Creating sample data  
                                    DataRow dr = dt.NewRow();
                                    dr["Aplicaciones"] = bm.labels[i].ToString();
                                    dr["Tiempo"] = bm.data[i];
                                    dt.Rows.Add(dr);
                        }   
                    }

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
    }
}
