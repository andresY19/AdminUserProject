using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Queue.Controllers
{
    [Authorize(Roles = "SAdmin,Admin,Manager,Employer,Employee")]
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            return View();
        }

        #region Register
        public async void SendInvitation(string email, string login, string pass)
        {
            await SendMail(email, SetEmailBody(email, login, pass), ConfigurationManager.AppSettings["EmailSubjec"]);
        }
        private AlternateView SetEmailBody(string email, string login, string pass)
        {
            try
            {
                //se arma el correo que se envia para el ambio de clave
                string ruta = ConfigurationManager.AppSettings["WelcomeTemplate"];

                string plantilla = Path.Combine(HttpRuntime.AppDomainAppPath, ruta);

                var html = System.IO.File.ReadAllText(plantilla);

                html = html.Replace("{{user}}", email);
                html = html.Replace("{{login}}", login);
                html = html.Replace("{{pass}}", pass);

                AlternateView av = AlternateView.CreateAlternateViewFromString(html, null, "text/html");

                return av;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Forgot
        public async void SendForgot(string email, string callback)
        {
            await SendMail(email, SetForgotEmailBody(email, callback), ConfigurationManager.AppSettings["EmailSubjectForgot"]);
        }
        private AlternateView SetForgotEmailBody(string email, string callback)
        {
            try
            {
                //se arma el correo que se envia para el ambio de clave
                string ruta = ConfigurationManager.AppSettings["ForgotTemplate"];

                string plantilla = Path.Combine(HttpRuntime.AppDomainAppPath, ruta);

                var html = System.IO.File.ReadAllText(plantilla);

                html = html.Replace("{{user}}", email);
                html = html.Replace("{{callback}}", callback);

                AlternateView av = AlternateView.CreateAlternateViewFromString(html, null, "text/html");

                return av;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        private async Task<bool> SendMail(string toAddress, AlternateView emailbody, string Subject)
        {
            try
            {
                var userCredentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUserName"].ToString(), ConfigurationManager.AppSettings["SMTPPassword"]);

                SmtpClient smtp = new SmtpClient
                {
                    Host = Convert.ToString(ConfigurationManager.AppSettings["SMTPHost"]),
                    Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]),
                    EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["SMTPEnableSsl"]),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPTimeout"])
                };
                smtp.Credentials = userCredentials;
                //smtp.UseDefaultCredentials = false;

                MailMessage message = new MailMessage();
                message.From = new MailAddress(ConfigurationManager.AppSettings["SenderEmailAddress"], ConfigurationManager.AppSettings["SenderDisplayName"]);
                message.Subject = Subject;
                message.IsBodyHtml = true;
                message.AlternateViews.Add(emailbody);
                message.IsBodyHtml = true;

                //message.To.Add(email);
                foreach (var m in toAddress.Split(','))
                {
                    message.To.Add(m);
                };

                smtp.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}