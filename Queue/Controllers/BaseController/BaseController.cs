using Queue.Utils;
using System.Web.Mvc;

namespace Queue.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public void Warning(string messagecode, string aditional)
        {
            if (!string.IsNullOrEmpty(aditional))
                TempData.Add(Alerts.WARNING, messagecode + " Detalle: " + aditional);
            else
                TempData.Add(Alerts.WARNING, messagecode);
        }
        public void Success(string messagecode)
        {
            TempData.Add(Alerts.SUCCESS, messagecode);
        }

        public void Information(string messagecode)
        {
            TempData.Add(Alerts.INFORMATION, messagecode);
        }

        public void Error(string messagecode, string aditional)
        {
            if (!string.IsNullOrEmpty(aditional))
                TempData.Add(Alerts.ERROR, messagecode + " Detalle: " + aditional);
            else
                TempData.Add(Alerts.ERROR, messagecode);
        }
    }
}
