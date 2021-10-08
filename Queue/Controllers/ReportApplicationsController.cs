using Newtonsoft.Json;
using Queue.DAL;
using Queue.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Queue.Controllers
{
    public class ReportApplicationsController : Controller
    {
        private QueueContext db = new QueueContext();
        // GET: ReportApplications
        public ActionResult Index()
        {
            try
            {
                ViewBag.DateFrom = DateTime.Now.AddDays(-2);
                ViewBag.DateTo = DateTime.Now;
                Guid idcompany = Guid.Parse(Request.RequestContext.HttpContext.Session["Company"].ToString());
                ViewBag.Department = db.Agent_CompanyDepartment.Where(x => x.IdCompany == idcompany).Distinct().ToList();

                return View();
            }
            catch (Exception err)
            {

                throw err;
            }

        }

        public JsonResult GetUser(string idDeparment)
        {
            try
            {
                var company = Request.RequestContext.HttpContext.Session["Company"].ToString();
                OperationController opc = new OperationController();
                var result = db.Agent_Employee.Where(d => d.Agent_CompanyDepartment.Id.ToString() == idDeparment).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        public JsonResult UserbyDepartamentData(string dateFrom, string dateTo, string idUser, string idDeparment)
        {
            try
            {

                var company = Request.RequestContext.HttpContext.Session["Company"].ToString();
                OperationController opc = new OperationController();
                DateTime fromdate = Convert.ToDateTime(dateFrom);
                DateTime todate = Convert.ToDateTime(dateTo);

                //BasicStatsModel bm = opc.MoreUsedApp(company, fromdate, todate);

                var UserByDeparment = db.Agent_Employee.Where(x => x.Agent_CompanyDepartment.Id.ToString() == idDeparment && x.idEmployee.ToString() == idUser)
                .Distinct().ToList();
                BasicUserModel User = opc.GetUserByName(company, fromdate, todate, UserByDeparment[0].Usuario);
                var ApplicationDist = User.Application.Distinct().ToList();

                ArrayUsersModel UserModel = new ArrayUsersModel();
                List<object> iData = new List<object>();
                Random rnd = new Random();
                string UpdateDate = "";
                double SumTime = 0;

                for (var k = 0; k < ApplicationDist.Count; k++)
                {
                    UserModel.Application.Add(ApplicationDist[k]);
                    for (var cont = 0; cont < User.User.Count; cont++)
                    {
                        if (User.Application[cont] == ApplicationDist[k])
                        {
                            SumTime = SumTime + User.Time[cont];
                        }
                    }
                    var round = Math.Round((SumTime / 3600), 2);
                    UserModel.Time.Add(round);
                    SumTime = 0;
                    UserModel.User = User.User[0];
                };
                //if (User.User[0].ToString()== idUser)
                //{
                //    UserModel.User = idUser;
                //}


                UpdateDate = JsonConvert.SerializeObject(UserModel);

                iData.Add(UpdateDate);


                return Json(iData, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public JsonResult NewChart(string dateFrom, string dateTo, string idUser, string idDeparment)
        {
            try
            {
                if (idUser == "0")
                {



                    var company = Request.RequestContext.HttpContext.Session["Company"].ToString();
                    OperationController opc = new OperationController();
                    DateTime fromdate = Convert.ToDateTime(dateFrom);
                    DateTime todate = Convert.ToDateTime(dateTo);
                    BasicUserModel user = opc.GetUsers(company, fromdate, todate);
                    //BasicStatsModel bm = opc.MoreUsedApp(company, fromdate, todate);

                    var UserByDeparment = db.Agent_Employee.Where(x => x.Agent_CompanyDepartment.Id.ToString() == idDeparment)
                    .Select(c => c.Usuario).Distinct().ToList();




                    var UserDist = user.User.Distinct().ToList();

                    List<string> UserFilter = new List<string>();

                    for (var contUser = 0; contUser < UserByDeparment.Count; contUser++)
                    {
                        for (var distinct = 0; distinct < UserDist.Count; distinct++)
                        {
                            if (UserByDeparment[contUser] == UserDist[distinct])
                            {
                                UserFilter.Add(UserDist[distinct]);
                            }
                        }
                    }

                    var ApplicationDist = user.Application.Distinct().ToList();

                    List<object> iData = new List<object>();
                    //ArrayUsersModel UserModel = new ArrayUsersModel();
                    Random rnd = new Random();
                    string UpdateDate = "";
                    double SumTime = 0;


                    for (var i = 0; i < UserFilter.Count; i++)
                    {
                        ArrayUsersModel UserModel = new ArrayUsersModel();
                        UserModel.User = UserFilter[i];

                        for (var k = 0; k < ApplicationDist.Count; k++)
                        {
                            UserModel.Application.Add(ApplicationDist[k]);
                            for (var cont = 0; cont < user.User.Count; cont++)
                            {
                                if (user.User[cont] == UserFilter[i] && user.Application[cont] == ApplicationDist[k])
                                {
                                    SumTime = SumTime + user.Time[cont];
                                }
                            }
                            var round = SumTime / 3600;
                            UserModel.Time.Add(round);
                            SumTime = 0;
                            //UserModel.Time.Add(rnd.Next(1, 50));
                        }

                        UpdateDate = JsonConvert.SerializeObject(UserModel);

                        iData.Add(UpdateDate);

                    }

                    return Json(iData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return UserbyDepartamentData(dateFrom, dateTo, idUser, idDeparment);

                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public ActionResult GaugeChart()
         {
            try
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
            catch (Exception err)
            {
                throw err;
            }

        }

        public JsonResult ChartWeb(string dateFrom, string dateTo)
        {
            try
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
            catch (Exception)
            {

                throw;
            }

        }

        public JsonResult ChartImproductive(string dateFrom, string dateTo)
        {
            try
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
                            if (p.clasification == 2)
                            {
                                //Creating sample data  
                                DataRow dr = dt.NewRow();
                                dr["Aplicaciones"] = bm.labels[i].ToString();
                                dr["Tiempo"] = bm.data[i] ;
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
            catch (Exception)
            {

                throw;
            }

        }
    }
}
