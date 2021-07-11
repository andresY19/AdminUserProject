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
    public class Agent_GroupHoraryController : BaseController
    {
        private QueueContext db = new QueueContext();

        // GET: Agent_GroupHorary
        public ActionResult Index()
        {
            return View(db.Agent_GroupHorary.ToList());
        }

        // GET: Agent_GroupHorary/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent_GroupHorary agent_GroupHorary = db.Agent_GroupHorary.Find(id);
            if (agent_GroupHorary == null)
            {
                return HttpNotFound();
            }
            return View(agent_GroupHorary);
        }

        // GET: Agent_GroupHorary/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agent_GroupHorary/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_GroupHorary,NameGroup")] Agent_GroupHorary agent_GroupHorary)
        {
            if (ModelState.IsValid)
            {
                if (db.Agent_GroupHorary.Where(d => d.NameGroup == agent_GroupHorary.NameGroup).ToList().Count == 0)
                {
                    agent_GroupHorary.Id_GroupHorary = Guid.NewGuid();
                    db.Agent_GroupHorary.Add(agent_GroupHorary);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else {
                    Warning("Grupo de horario ya existe", string.Empty);
                }
                
            }

            return View(agent_GroupHorary);
        }

        // GET: Agent_GroupHorary/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent_GroupHorary agent_GroupHorary = db.Agent_GroupHorary.Find(id);
            if (agent_GroupHorary == null)
            {
                return HttpNotFound();
            }
            return View(agent_GroupHorary);
        }

        // POST: Agent_GroupHorary/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_GroupHorary,NameGroup")] Agent_GroupHorary agent_GroupHorary)
        {
            if (ModelState.IsValid)
            {
                if (db.Agent_GroupHorary.Where(d => d.NameGroup == agent_GroupHorary.NameGroup).ToList().Count == 0)
                {
                    db.Entry(agent_GroupHorary).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else {
                    Warning("Grupo de horario ya existe", string.Empty);
                }
            }
            return View(agent_GroupHorary);
        }

        // GET: Agent_GroupHorary/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent_GroupHorary agent_GroupHorary = db.Agent_GroupHorary.Find(id);
            if (agent_GroupHorary == null)
            {
                return HttpNotFound();
            }
            return View(agent_GroupHorary);
        }

        // POST: Agent_GroupHorary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (db.Agent_GroupHoraryDetail.Where(g => g.Id_GroupHorary == id) != null)
            {
                Agent_GroupHorary agent_GroupHorary = db.Agent_GroupHorary.Find(id);
                db.Agent_GroupHorary.Remove(agent_GroupHorary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else {
                Warning("Grupo de horario tiene detalle de horarios", string.Empty);
            }
            return View();
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
