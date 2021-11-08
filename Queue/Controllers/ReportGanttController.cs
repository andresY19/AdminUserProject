using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Queue.Controllers
{
    public class ReportGanttController : Controller
    {
        // GET: ReportGantt
        public ActionResult Index()
        {
            return View();
        }

        // GET: ReportGantt/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReportGantt/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReportGantt/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ReportGantt/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReportGantt/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ReportGantt/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReportGantt/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
