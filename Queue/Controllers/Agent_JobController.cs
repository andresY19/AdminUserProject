using Queue.DAL;
using Queue.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Queue.Controllers
{
    [Authorize]
    public class Agent_JobController : BaseController
    {
        private QueueContext db = new QueueContext();

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var company = Request.RequestContext.HttpContext.Session["Company"].ToString();
            if (company != null)
            {
                var guidCompany = Guid.Parse(company);
                return View(db.Agent_Job.Where(j => j.IdCompany == guidCompany).ToList());
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Agent_Job agent_Job)
        {
            if (ModelState.IsValid)
            {
                var company = Guid.Parse(Request.RequestContext.HttpContext.Session["Company"].ToString());

                agent_Job.idJob = Guid.NewGuid();
                agent_Job.IdCompany = company;
                db.Agent_Job.Add(agent_Job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(agent_Job);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent_Job agent_Job = db.Agent_Job.Find(id);
            if (agent_Job == null)
            {
                return HttpNotFound();
            }
            ViewBag.status = agent_Job.status;
            return View(agent_Job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Agent_Job agent_Job)
        {
            var company = Guid.Parse(Request.RequestContext.HttpContext.Session["Company"].ToString());
            if (ModelState.IsValid)
            {
                int exists = db.Agent_Job.Where(t => t.IdCompany == company && t.Cargo == agent_Job.Cargo && t.idJob != agent_Job.idJob).Count();

                if (exists <= 0)
                {
                    agent_Job.IdCompany = company;
                    db.Entry(agent_Job).State = EntityState.Modified;

                    if (agent_Job.string_status == "Inactive")
                        agent_Job.status = false;
                    else
                        agent_Job.status = true;

                    db.SaveChanges();
                    Success("Registro editado con exito");
                    return RedirectToAction("Index");
                }
                else
                    Warning("Cargo ya existe", string.Empty);
            }
            return View(agent_Job);
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
