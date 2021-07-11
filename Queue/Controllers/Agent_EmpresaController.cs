using Microsoft.AspNet.Identity.Owin;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
        private ApplicationUserManager _userManager;

        public Agent_EmpresaController()
        {

        }

        public Agent_EmpresaController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        private ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        [Authorize(Roles = "SAdmin")]
        public ActionResult Index()
        {
            return View(db.Agent_Empresa.ToList());
        }

        [Authorize(Roles = "SAdmin")]
        public ActionResult Create()
        {
            return View();
        }

        


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SAdmin")]
        public async Task<ActionResult> Create(Agent_Empresa agent_Empresa)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //variables iniciales
                    var CompanyId = Guid.NewGuid();

                    //Creacion de la empresa
                    agent_Empresa.IdCompany = CompanyId;
                    db.Agent_Empresa.Add(agent_Empresa);

                    //Crear Usuario Admnistrador a la empresa
                    var _username = String.Concat("Ad_", agent_Empresa.Nombre.Replace(" ","").Replace(".","").ToLower().ToString());
                    var user = new ApplicationUser { UserName = _username, Email = agent_Empresa.Email, FirstName = agent_Empresa.Nombre, LastName = agent_Empresa.Rut };
                    var Password = System.Web.Security.Membership.GeneratePassword(8, 1);
                    var result = await UserManager.CreateAsync(user, Password);
                    if (result.Succeeded)
                    {
                        var Role = db.Roles.Where(r => r.Name.Equals("Admin")).SingleOrDefault();
                        await UserManager.AddToRoleAsync(user.Id, Role.Name);

                        var oUser = db.Users.Find(user.Id);
                        oUser.EmailConfirmed = true;
                        oUser.LockoutEnabled = false;

                        db.Entry(oUser).State = EntityState.Modified;

                        var ec = new EmailController();
                        ec.SendInvitation(oUser.Email, oUser.Email, Password);

                        await db.SaveChangesAsync();

                        var relation = new Agent_UserCompanies()
                        {
                            idUser = Guid.Parse(oUser.Id),
                            IdCompany = CompanyId,
                        };

                        db.Agent_UserCompany.Add(relation);
                        db.SaveChanges();
                    }
                }
                //Rollback
                catch (Exception ex)
                {
                    if (agent_Empresa != null)
                    {
                        db.Agent_Empresa.Remove(agent_Empresa);
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index","Agent_Empresa");
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
