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
    [Authorize]
    public class Agent_EmpresaController : Controller
    {
        private QueueContext db = new QueueContext();

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Agent_Empresa.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Agent_Empresa agent_Empresa)
        {
            if (ModelState.IsValid)
            {
                agent_Empresa.IdCompany = Guid.NewGuid();
                db.Agent_Empresa.Add(agent_Empresa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(agent_Empresa);
        }
                
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent_Empresa agent_Empresa = db.Agent_Empresa.Find(id);
            if (agent_Empresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.status = agent_Empresa.status;
            return View(agent_Empresa);
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Agent_Empresa agent_Empresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agent_Empresa).State = EntityState.Modified;

                if (agent_Empresa.string_status == "Inactive")
                    agent_Empresa.status = false;
                else
                    agent_Empresa.status = true;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agent_Empresa);
        }        
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent_Empresa agent_Empresa = db.Agent_Empresa.Find(id);
            if (agent_Empresa == null)
            {
                return HttpNotFound();
            }
            return View(agent_Empresa);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Agent_Empresa agent_Empresa = db.Agent_Empresa.Find(id);
            db.Agent_Empresa.Remove(agent_Empresa);
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
