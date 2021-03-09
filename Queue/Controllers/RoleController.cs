using Microsoft.AspNet.Identity.Owin;
using Queue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Queue.Controllers
{
    public class RoleController : Controller
    {
        private ApplicationRoleManager _roleManager;

        public RoleController()
        {
        }

        public RoleController(ApplicationRoleManager roleManager)
        {
            RoleManager = roleManager;
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        // GET: Role
        public ActionResult Index()
        {
            List<RoleViewModel> list = new List<RoleViewModel>();
            foreach (var role in RoleManager.Roles)
                list.Add(new RoleViewModel(role));

            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel model)
        {
            var RoleExist = false;
            var role = new ApplicationRole() { Name = model.Name };
            foreach (var _role in RoleManager.Roles)
                if (_role.Name == model.Name)
                {
                    RoleExist = true;
                }

            if (!RoleExist)
            {
                await RoleManager.CreateAsync(role);
                return RedirectToAction("Index");
            }
            else
            {
                return Content("Duplicate Role");
            }
                
            
        }

        public async Task<ActionResult> Edit(string Id)
        {
            var role = await RoleManager.FindByIdAsync(Id);
            return View(new RoleViewModel(role));
        }

        [HttpPost]
        public async Task<ActionResult>Edit(RoleViewModel model)
        {
            /*ApplicationRole role = await*/
            var role = new ApplicationRole() { Id = model.Id, Name = model.Name };
            Boolean RoleExist = false;
            foreach (var _role in RoleManager.Roles)
                if (_role.Name == model.Name)
                {
                    RoleExist = true;
                }
            if (!RoleExist)
            {
                await RoleManager.UpdateAsync(role);
                return RedirectToAction("Index");
            }
            else
            {
                return Content("Duplicate Role");
            }
        }

        public async Task<ActionResult> Details(string Id)
        {
            var role = await RoleManager.FindByIdAsync(Id);
            return View(new RoleViewModel(role));
        }
        public async Task<ActionResult> Delete(string Id)
        {
            var role = await RoleManager.FindByIdAsync(Id);
            return View(new RoleViewModel(role));
        }

        public async Task<ActionResult> DeleteConfirmed(string Id)
        {
            var role = await RoleManager.FindByIdAsync(Id);
            await RoleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }



    }
}