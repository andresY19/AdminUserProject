using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Queue.DAL;
using Queue.Models;

namespace Queue.Controllers
{
    [Authorize(Roles = "Admin")]
    public class Agent_ConfigurationController : Controller
    {
        private QueueContext db = new QueueContext();

        // GET: Agent_Configuration
        public ActionResult Index()
        {
            return View(db.Agent_Configuration.ToList());
        }

        // GET: Agent_Configuration/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent_Configuration agent_Configuration = db.Agent_Configuration.Find(id);
            if (agent_Configuration == null)
            {
                return HttpNotFound();
            }
            return View(agent_Configuration);
        }

        // GET: Agent_Configuration/Create
        public ActionResult Create(Guid id)
        {
            Agent_Configuration ac = db.Agent_Configuration.Where(d => d.IdCompany == id).SingleOrDefault();

            if (ac != null)
                return RedirectToAction("Edit", new { id = ac.Id_Configuration });
            else
            {
                ac = new Agent_Configuration();
                ac.IdCompany = id;
                ViewBag.IdCompany = id;
            }

            return View(ac);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Agent_Configuration ac)
        {
            if (ModelState.IsValid)
            {
                ac.Id_Configuration = Guid.NewGuid();
                ac.Agent_Empresa = db.Agent_Empresa.Where(a => a.IdCompany == ac.IdCompany).SingleOrDefault();

                db.Agent_Configuration.Add(ac);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = ac.Id_Configuration });
            }

            return View(ac);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent_Configuration agent_Configuration = db.Agent_Configuration.Find(id);
            if (agent_Configuration == null)
            {
                return HttpNotFound();
            }
            return View(agent_Configuration);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Agent_Configuration ac)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ac).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Agent_Empresa", new { id = ac.IdCompany });
            }
            return View(ac);
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
