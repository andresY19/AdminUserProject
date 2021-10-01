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
using static Queue.Utils.Enums;

namespace Queue.Controllers
{
    public class Agent_GroupHoraryDetailController : BaseController
    {
        private QueueContext db = new QueueContext();

        // GET: Agent_GroupHoraryDetail
        public ActionResult Index(Guid id)
        {
            var agent_GroupHoraryDetail = db.Agent_GroupHoraryDetail.Include(a => a.Agent_GroupHorary);
            ViewBag.IdGroupHorary = id;
            return View(agent_GroupHoraryDetail.Where(x => x.Id_GroupHorary == id).ToList());
        }

        // GET: Agent_GroupHoraryDetail/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent_GroupHoraryDetail agent_GroupHoraryDetail = db.Agent_GroupHoraryDetail.Find(id);
            if (agent_GroupHoraryDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdGroupHorary = id;
            return View(agent_GroupHoraryDetail);
        }

        // GET: Agent_GroupHoraryDetail/Create
        public ActionResult Create(Guid? id)
        {
            try
            {
                ViewBag.Id_GroupHorary = new SelectList(db.Agent_GroupHorary, "Id_GroupHorary", "NameGroup");
                Agent_GroupHoraryDetail agent_GroupHoraryDetail = new Agent_GroupHoraryDetail();
                agent_GroupHoraryDetail.Id_GroupHorary = (Guid)id;
                ViewBag.IdGroupHorary = id;
                return View(agent_GroupHoraryDetail);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        // POST: Agent_GroupHoraryDetail/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_GroupHoraryDetail,Day,HourFrom,HourUntil,Type,Id_GroupHorary")] Agent_GroupHoraryDetail agent_GroupHoraryDetail)
        {
            if (ModelState.IsValid)
            {
                if (agent_GroupHoraryDetail.Type != EnumType.Seleccione)
                {
                    if ((agent_GroupHoraryDetail.HourFrom < agent_GroupHoraryDetail.HourUntil))
                    {
                        string message = _validateHours(agent_GroupHoraryDetail.HourFrom, agent_GroupHoraryDetail.HourUntil, db.Agent_GroupHoraryDetail.Where(x => x.Day == agent_GroupHoraryDetail.Day).ToList());
                        if (string.IsNullOrEmpty(message))
                        {
                            agent_GroupHoraryDetail.Id_GroupHoraryDetail = Guid.NewGuid();
                            db.Agent_GroupHoraryDetail.Add(agent_GroupHoraryDetail);
                            db.SaveChanges();
                            return RedirectToAction(nameof(Index), new { id = agent_GroupHoraryDetail.Id_GroupHorary });
                        }
                        else
                        {
                            Warning(message, string.Empty);
                        }
                    }
                    else
                    {
                        Warning("Hora hasta no puede ser mayor a la hora desde", string.Empty);
                    }
                }
                else
                {
                    Warning("Tiene que seleccionar un tipo", string.Empty);
                }
            }
            ViewBag.IdGroupHorary = agent_GroupHoraryDetail.Id_GroupHorary;
            ViewBag.Id_GroupHorary = new SelectList(db.Agent_GroupHorary, "Id_GroupHorary", "NameGroup", agent_GroupHoraryDetail.Id_GroupHorary);
            return View(agent_GroupHoraryDetail);
        }

        // GET: Agent_GroupHoraryDetail/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent_GroupHoraryDetail agent_GroupHoraryDetail = db.Agent_GroupHoraryDetail.Find(id);
            if (agent_GroupHoraryDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdGroupHorary = agent_GroupHoraryDetail.Id_GroupHorary;
            ViewBag.Id_GroupHorary = new SelectList(db.Agent_GroupHorary, "Id_GroupHorary", "NameGroup", agent_GroupHoraryDetail.Id_GroupHorary);
            return View(agent_GroupHoraryDetail);
        }

        // POST: Agent_GroupHoraryDetail/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Agent_GroupHoraryDetail agent_GroupHoraryDetail)
        {
            if (ModelState.IsValid)
            {
                if (agent_GroupHoraryDetail.Type != EnumType.Seleccione)
                {
                    if (!(agent_GroupHoraryDetail.HourUntil < agent_GroupHoraryDetail.HourFrom))
                    {
                        //si es un dia diferente a este que no permita actualizar 
                        if (!(db.Agent_GroupHoraryDetail.Where(x => x.Id_GroupHorary == agent_GroupHoraryDetail.Id_GroupHorary && x.Id_GroupHoraryDetail != agent_GroupHoraryDetail.Id_GroupHoraryDetail && x.Day == agent_GroupHoraryDetail.Day).Count() > 0))
                        {
                            Agent_GroupHoraryDetail agent_GroupHoraryDetail1 = db.Agent_GroupHoraryDetail.Where(d => d.Id_GroupHoraryDetail == agent_GroupHoraryDetail.Id_GroupHoraryDetail).SingleOrDefault();
                            // db.Entry(agent_GroupHoraryDetail1).CurrentValues.SetValues(agent_GroupHoraryDetail);
                            agent_GroupHoraryDetail1.HourUntil = agent_GroupHoraryDetail.HourUntil;
                            agent_GroupHoraryDetail1.HourFrom = agent_GroupHoraryDetail.HourFrom;
                            agent_GroupHoraryDetail1.Day = agent_GroupHoraryDetail.Day;

                            db.SaveChanges();
                            return RedirectToAction(nameof(Index), new { id = agent_GroupHoraryDetail.Id_GroupHorary });
                        }
                        else
                        {
                            string message = " Ya existe un horario con los datos registrados para el "+ agent_GroupHoraryDetail.Day;
                            Warning(message, string.Empty);
                        }
                    }
                    else
                    {
                        Warning("Hora hasta no puede ser mejor a la hora desde", string.Empty);
                    }
                }
                else
                {
                    Warning("Tiene que seleccionar un tipo", string.Empty);
                }
            }
            ViewBag.IdGroupHorary = agent_GroupHoraryDetail.Id_GroupHorary;
            ViewBag.Id_GroupHorary = new SelectList(db.Agent_GroupHorary, "Id_GroupHorary", "NameGroup", agent_GroupHoraryDetail.Id_GroupHorary);
            return View(agent_GroupHoraryDetail);
        }

        public string _validateHours(DateTime HourFrom, DateTime HourUntil, List<Agent_GroupHoraryDetail> lstAgent_GroupHoraryDetail)
        {
            string _message = string.Empty;

            if (lstAgent_GroupHoraryDetail.Where(x => x.HourFrom >= HourFrom && x.HourUntil <= HourUntil).Count() > 0)
            {
                _message = "Ya existe un rango de horario con los datos registrados";
            }
            if (lstAgent_GroupHoraryDetail.Where(x => x.HourFrom <= HourFrom && x.HourUntil <= HourUntil).Count() > 0)
            {
                _message = "Ya existe un rango de horario con los datos registrados";
            }

            return _message;
        }

        // GET: Agent_GroupHoraryDetail/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent_GroupHoraryDetail agent_GroupHoraryDetail = db.Agent_GroupHoraryDetail.Find(id);
            if (agent_GroupHoraryDetail == null)
            {
                return HttpNotFound();
            }
            return View(agent_GroupHoraryDetail);
        }

        // POST: Agent_GroupHoraryDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Agent_GroupHoraryDetail agent_GroupHoraryDetail = db.Agent_GroupHoraryDetail.Find(id);
            db.Agent_GroupHoraryDetail.Remove(agent_GroupHoraryDetail);
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
