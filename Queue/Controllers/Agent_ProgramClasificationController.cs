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
using Queue.Controllers;

namespace Queue.Controllers
{
    [Authorize]
    public class Agent_ProgramClasificationController : BaseController
    {
        private QueueContext db = new QueueContext();

        // GET: Agent_ProgramClasification
        public ActionResult Index()
        {
            Guid idcompany = Guid.Parse(Request.RequestContext.HttpContext.Session["Company"].ToString());

            return View(db.Agent_ProgramClasification.Where(t => t.Agent_Empresa.IdCompany == idcompany).ToList());
        }


        // GET: Agent_ProgramClasification/Create
        public ActionResult Create()
        {
            Agent_ProgramClasification program_clasification = new Agent_ProgramClasification();
            OperationController opc = new OperationController();
            Guid icompany = Guid.Parse(Request.RequestContext.HttpContext.Session["Company"].ToString());
            List<AutomaticTakeTimeModel> added = db.Agent_ProgramClasification.Where(r => r.Agent_Empresa.IdCompany == icompany).Select(t => new AutomaticTakeTimeModel
            {
                Application = t.name
            }).ToList();

            List<AutomaticTakeTimeModel> programs = opc.GetSoftWareClasification(icompany.ToString());

            program_clasification.AutomaticTakeTime = programs;
            List<SelectListItem> sli = new List<SelectListItem>();

            foreach (var i in programs.OrderBy(o => o.Application))
            {
                if (added.Where(t => t.Application == i.Application).Count() <= 0)
                {
                    SelectListItem si = new SelectListItem();
                    si.Text = i.Application;
                    si.Value = i.Application;
                    sli.Add(si);
                }
            }

            SelectList sl = new SelectList(sli);

            if (sl.Count() <= 0)
                ViewBag.noprograms = "Ya no hay mas programas para clasificar";
            ViewBag.name = sl.Items;

            return View(program_clasification);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Agent_ProgramClasification agent_ProgramClasification)
        {
            if (ModelState.IsValid)
            {
                var idcompany = Guid.Parse(Request.RequestContext.HttpContext.Session["Company"].ToString());
                agent_ProgramClasification.idprogramclasification = Guid.NewGuid();
                agent_ProgramClasification.Agent_Empresa = db.Agent_Empresa.Where(e => e.IdCompany == idcompany).SingleOrDefault();
                db.Agent_ProgramClasification.Add(agent_ProgramClasification);
                db.SaveChanges();
                Success("Registro creado con exito");
                return RedirectToAction("Index");
            }

            Agent_ProgramClasification program_clasification = new Agent_ProgramClasification();
            OperationController opc = new OperationController();
            Guid icompany = Guid.Parse(Request.RequestContext.HttpContext.Session["Company"].ToString());
            List<AutomaticTakeTimeModel> added = db.Agent_ProgramClasification.Where(r => r.Agent_Empresa.IdCompany == icompany).Select(t => new AutomaticTakeTimeModel
            {
                Application = t.name
            }).ToList();

            List<AutomaticTakeTimeModel> programs = opc.GetSoftWareClasification(icompany.ToString());

            program_clasification.AutomaticTakeTime = programs;
            List<SelectListItem> sli = new List<SelectListItem>();

            foreach (var i in programs.OrderBy(o => o.Application))
            {
                if (added.Where(t => t.Application == i.Application).Count() <= 0)
                {
                    SelectListItem si = new SelectListItem();
                    si.Text = i.Application;
                    si.Value = i.Application;
                    if (agent_ProgramClasification.name == i.Application)
                        si.Selected = true;

                    sli.Add(si);
                }
            }

            SelectList sl = new SelectList(sli);

            if (sl.Count() <= 0)
                ViewBag.noprograms = "Ya no hay mas programas para clasificar";

            ViewBag.name = sl.Items;

            return View(agent_ProgramClasification);
        }

        // GET: Agent_ProgramClasification/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent_ProgramClasification agent_ProgramClasification = db.Agent_ProgramClasification.Find(id);
            if (agent_ProgramClasification == null)
            {
                return HttpNotFound();
            }
            Agent_ProgramClasification program_clasification = new Agent_ProgramClasification();
            OperationController opc = new OperationController();

            List<AutomaticTakeTimeModel> programs = opc.GetSoftWareClasification(Request.RequestContext.HttpContext.Session["Company"].ToString());
            program_clasification.AutomaticTakeTime = programs;
            List<SelectListItem> sli = new List<SelectListItem>();

            foreach (var i in programs.OrderBy(o => o.Application))
            {
                SelectListItem si = new SelectListItem();
                si.Text = i.Application;
                si.Value = i.Application;
                if (agent_ProgramClasification.name == i.Application)
                    si.Selected = true;

                sli.Add(si);
            }

            SelectList sl = new SelectList(sli);

            ViewBag.name = sl.Items;
            ViewBag.clasification = agent_ProgramClasification.clasification;
            ViewBag.name_ = agent_ProgramClasification.name;


            return View(agent_ProgramClasification);
        }

        // POST: Agent_ProgramClasification/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Agent_ProgramClasification agent_ProgramClasification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agent_ProgramClasification).State = EntityState.Modified;
                db.SaveChanges();
                Success("Registro editado con exito");
                return RedirectToAction("Index");
            }

            Agent_ProgramClasification program_clasification = new Agent_ProgramClasification();
            OperationController opc = new OperationController();

            List<AutomaticTakeTimeModel> programs = opc.GetSoftWareClasification(Request.RequestContext.HttpContext.Session["Company"].ToString());
            program_clasification.AutomaticTakeTime = programs;
            List<SelectListItem> sli = new List<SelectListItem>();

            foreach (var i in programs.OrderBy(o => o.Application))
            {
                SelectListItem si = new SelectListItem();
                si.Text = i.Application;
                si.Value = i.Application;
                sli.Add(si);
            }

            SelectList sl = new SelectList(sli);

            ViewBag.name = sl.Items;

            return View(agent_ProgramClasification);
        }

        // GET: Agent_ProgramClasification/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent_ProgramClasification agent_ProgramClasification = db.Agent_ProgramClasification.Find(id);
            if (agent_ProgramClasification == null)
            {
                return HttpNotFound();
            }
            return View(agent_ProgramClasification);
        }

        // POST: Agent_ProgramClasification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Agent_ProgramClasification agent_ProgramClasification = db.Agent_ProgramClasification.Find(id);
            db.Agent_ProgramClasification.Remove(agent_ProgramClasification);
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
