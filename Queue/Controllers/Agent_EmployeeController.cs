using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Queue.Controllers.Token;
using Queue.DAL;
using Queue.Models;
using System.Collections.Generic;
using Queue.ViewModels;

namespace Queue.Controllers
{
    [Authorize]
    public class Agent_EmployeeController : BaseController
    {
        private QueueContext db = new QueueContext();
        private TokenValidationHandler tvh = new TokenValidationHandler();
        ClaimsPrincipal cp = new ClaimsPrincipal();

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var company = Request.RequestContext.HttpContext.Session["Company"].ToString();
            if (company != null)
            {
                var guidCompany = Guid.Parse(company);
                return View(db.Agent_Employee.Where(e => e.IdCompany == guidCompany).ToList());
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "SAdmin,Admin")]
        public ActionResult Create()
        {
            ViewBag.Jobs = db.Agent_Job.ToList();
            ViewBag.Horarys = db.Agent_GroupHoraryDetail.Include(x => x.Agent_GroupHorary).Select(g => g.Agent_GroupHorary).Distinct().OrderBy(d => d.NameGroup).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Agent_Employee agent_Employee)
        {
            Guid idcompany = Guid.Parse(Request.RequestContext.HttpContext.Session["Company"].ToString());
            if (ModelState.IsValid)
            {
                if (db.Agent_Employee.Where(t => t.IdCompany == idcompany && t.Identificacion == agent_Employee.Identificacion).Count() <= 0)
                {
                    if (db.Agent_Employee.Where(t => t.IdCompany == idcompany && t.Usuario == agent_Employee.Usuario).Count() <= 0)
                    {
                        if (db.Agent_Employee.Where(t => t.IdCompany == idcompany && t.Email == agent_Employee.Email).Count() <= 0)
                        {
                            agent_Employee.IdCompany = idcompany;
                            agent_Employee.idEmployee = Guid.NewGuid();
                            db.Agent_Employee.Add(agent_Employee);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                            Warning("Email ya existe", string.Empty);
                    }
                    else
                        Warning("Nombre Usuario ya existe", string.Empty);
                }
                else
                    Warning("La identificación del Usuario ya existe", string.Empty);
            }
            ViewBag.Horarys = db.Agent_GroupHoraryDetail.Include(x => x.Agent_GroupHorary).Select(g => g.Agent_GroupHorary).Distinct().OrderBy(d => d.NameGroup).ToList();
            ViewBag.Jobs = db.Agent_Job.ToList();
            

            return View(agent_Employee);
        }


        public ActionResult uploaduser(List<Agent_Employee_ViewModel> notuploaded)
        {
            return View(notuploaded);
        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult uploaduser(HttpPostedFileBase file)
        {
            List<Agent_Employee_ViewModel> notuploaded = new List<Agent_Employee_ViewModel>();
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var Pathfile = string.Concat("~", @"\", ConfigurationManager.AppSettings["FilesRepository"].ToString().Replace("/", @"\"), @"\");
                var userfolderpath = Path.Combine(Server.MapPath(Pathfile), fileName);
                try
                {
                    file.SaveAs(userfolderpath);

                    if (CreateByFile(userfolderpath, ref notuploaded))
                    {
                        Success("Archivo procesado exitosamente");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                Warning("Archivo vacio o con formato no aceptado", string.Empty);
            }


            return View(notuploaded);
        }

        [Authorize(Roles = "Admin")]
        private Boolean CreateByFile(String FilePath, ref List<Agent_Employee_ViewModel> notuploaded)
        {
            if (System.IO.File.Exists(FilePath))
            {
                Guid company = Guid.Parse(Request.RequestContext.HttpContext.Session["Company"].ToString());
                var separator = ConfigurationManager.AppSettings["CSV_Separator"].ToString().ToCharArray();
                var Company = Guid.Parse(Request.RequestContext.HttpContext.Session["Company"].ToString());
                try
                {
                    string[] lines = System.IO.File.ReadAllLines(FilePath);
                    for (int i = 1; i < lines.Count(); i++)
                    {
                        bool error = false;
                        string posibleerror = string.Empty;
                        var line = lines[i].Split(separator);

                        if (line.Length == 7)
                        {
                            var cargo = line[5].ToString();
                            var guid_Cargo = db.Agent_Job.Where(j => j.Cargo == cargo && j.IdCompany == company).Select(t => t.idJob).FirstOrDefault();

                            if (guid_Cargo == null || guid_Cargo == Guid.Empty)
                            {
                                error = true;
                                guid_Cargo = Guid.Empty;
                                posibleerror = "Cargo No Existe";
                            }

                            var identificacion = line[4].ToString();
                            var existeid = db.Agent_Employee.Where(r => r.IdCompany == Company && r.Identificacion == identificacion && r.Cargo == guid_Cargo).Count();

                            if (existeid > 0)
                            {
                                error = true;
                                posibleerror = "Identificación y cargo ya existen";
                            }

                            var user_ = line[6].Replace("\"", "");
                            var existeuser = db.Agent_Employee.Where(r => r.IdCompany == Company && r.Usuario == user_).Count();

                            if (existeuser > 0)
                            {
                                error = true;
                                posibleerror = "Usuario ya existe";
                            }

                            try
                            {
                                if (error)
                                {
                                    var e = new Agent_Employee_ViewModel()
                                    {
                                        Nombre = line[0].Replace("\"", ""),
                                        Direccion = line[1],
                                        Telefono = line[2],
                                        Email = line[3].ToLower(),
                                        Identificacion = line[4],
                                        Cargo = line[5].ToString(),
                                        Usuario = line[6].Replace("\"", ""),
                                        posibleerror = posibleerror
                                    };

                                    notuploaded.Add(e);
                                }
                                else
                                {
                                    var e = new Agent_Employee()
                                    {
                                        idEmployee = Guid.NewGuid(),
                                        IdCompany = Company,
                                        Nombre = line[0].Replace("\"", ""),
                                        Direccion = line[1],
                                        Telefono = line[2],
                                        Email = line[3].ToLower(),
                                        Identificacion = line[4],
                                        Cargo = guid_Cargo,
                                        Usuario = line[6].Replace("\"", ""),
                                        status = true,
                                        string_status = "Active"
                                    };
                                    db.Agent_Employee.Add(e);
                                    db.SaveChanges();
                                }
                            }
                            catch (Exception)
                            {
                                Warning("Ocurio un error en la carga", string.Empty);
                            }
                        }
                        else
                        {
                            Warning("Archivo con formato no aceptado, o algunas columnas estan mal formateadas", string.Empty);
                        }
                    }
                    return true;
                }
                catch (Exception e)
                {
                    Warning("Ocurio un error en la carga", string.Empty);
                }
                finally
                {
                    System.IO.File.Delete(FilePath);
                }
            }

            return false;
        }


        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent_Employee agent_Employee = db.Agent_Employee.Find(id);
            if (agent_Employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.status = agent_Employee.status;
            ViewBag.Jobs = db.Agent_Job.ToList();
            ViewBag.Horarys = db.Agent_GroupHoraryDetail.Include(x => x.Agent_GroupHorary).Select(g => g.Agent_GroupHorary).Distinct().OrderBy(d => d.NameGroup).ToList();
            return View(agent_Employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Agent_Employee agent_Employee)
        {
            Guid idcompany = Guid.Parse(Request.RequestContext.HttpContext.Session["Company"].ToString());
            if (ModelState.IsValid)
            {
                if (db.Agent_Employee.Where(t => t.IdCompany == idcompany && t.Usuario == agent_Employee.Usuario && t.idEmployee != agent_Employee.idEmployee).Count() <= 0)
                {
                    if (db.Agent_Employee.Where(t => t.IdCompany == idcompany && t.Email == agent_Employee.Email && t.idEmployee != agent_Employee.idEmployee).Count() <= 0)
                    {
                        agent_Employee.IdCompany = idcompany;
                        db.Entry(agent_Employee).State = EntityState.Modified;

                        if (agent_Employee.string_status == "Inactive")
                            agent_Employee.status = false;
                        else
                            agent_Employee.status = true;

                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                        Warning("Email ya existe", string.Empty);
                }
                else
                    Warning("Nombre Usuario ya existe", string.Empty);
            }

            ViewBag.status = agent_Employee.status;
            ViewBag.Jobs = db.Agent_Job.ToList();
            ViewBag.Horarys = db.Agent_GroupHoraryDetail.Include(x => x.Agent_GroupHorary).Select(g => g.Agent_GroupHorary).Distinct().OrderBy(d => d.NameGroup).ToList();
            return View(agent_Employee);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent_Employee agent_Employee = db.Agent_Employee.Find(id);
            if (agent_Employee == null)
            {
                return HttpNotFound();
            }
            return View(agent_Employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Agent_Employee agent_Employee = db.Agent_Employee.Find(id);
            db.Agent_Employee.Remove(agent_Employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
