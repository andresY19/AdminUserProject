using Queue.App_Start;
using Queue.DAL;
using System.Web.Mvc;

namespace Queue.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private QueueContext db = new QueueContext();

        [SessionAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [SessionAuthorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [SessionAuthorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}