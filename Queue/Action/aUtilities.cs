using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using Queue.Models;
using Queue.DAL;

namespace Queue.Action
{
    public class aUtilities
    {
        public HttpResponseMessage ReturnResponse(object o)
        {
            HttpResponseMessage httpResponseMessage = null;
            StringContent content = null;

            content = new StringContent(JsonConvert.SerializeObject(o, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd/MM/yyyy" }));
            httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            httpResponseMessage.Content = content;
            return httpResponseMessage;
        }
        public List<ErrorFields> ValidateObject(object t)
        {
            List<ErrorFields> rl = new List<ErrorFields>();
            try
            {
                ValidationContext context = new ValidationContext(t, null, null);
                List<ValidationResult> results = new List<ValidationResult>();
                bool valid = Validator.TryValidateObject(t, context, results, true);

                if (!valid)
                {
                    foreach (ValidationResult vr in results)
                    {
                        ErrorFields r = new ErrorFields();
                        // r.field = vr.MemberNames.First();
                        r.message = vr.ErrorMessage;
                        rl.Add(r);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorFields r = new ErrorFields();
                r.message = ex.Message;
                rl.Add(r);
            }
            return rl;
        }
        public string Sha(string pass)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(pass);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);

            return hash;
        }
        public Response ReturnMesagge(ref Response ret, int idmensje, string custom, Guid? id, HttpStatusCode status = HttpStatusCode.OK)
        {
            using (QueueContext ent = new QueueContext())
            {
                try
                {
                    Agent_GenericError ge = (from g in ent.Agent_GenericError
                                             where g.codigo_id == idmensje
                                       select g).SingleOrDefault();

                    ret.status = status;
                    ret.response_code = ge.Codigo;
                    ret.message = ge.Message + " " + custom;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return ret;
        }
        public Response ReturnMesagge(ref Response ret, int idmensje, string custom, Guid? id, string rute, HttpStatusCode status = HttpStatusCode.OK)
        {
            using (QueueContext ent = new QueueContext())
            {
                Agent_GenericError ge = (from g in ent.Agent_GenericError
                                         where g.codigo_id == idmensje
                                   select g).SingleOrDefault();

                ret.status = status;
                ret.response_code = ge.Codigo;
                ret.message = ge.Message + " " + custom;
                ret.rute = rute;
            }

            return ret;
        }
        public Response ReturnMesagge(ref Response ret, int idmensje, string custom, Guid? id, List<ErrorFields> el, HttpStatusCode status = HttpStatusCode.OK)
        {
            using (QueueContext ent = new QueueContext())
            {
                Agent_GenericError ge = (from g in ent.Agent_GenericError
                                         where g.codigo_id == idmensje
                                   select g).SingleOrDefault();

                ret.status = status;
                ret.response_code = ge.Codigo;
                ret.message = ge.Message + " " + custom;

                ret.error.AddRange(el);
            }

            return ret;
        }
    }
}