using Queue.Action;
using Queue.DAL;
using Queue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Queue.Controllers
{
    public class ScheduleController : ApiController
    {
        aUtilities ut = new aUtilities();
        private QueueContext db = new QueueContext();

        [HttpPost]
        [Route("Get")]
        public HttpResponseMessage Schedule(schedule t)
        {

            var today = DateTime.Now;
            int day = ((int)today.DayOfWeek == 0) ? 7 : (int)today.DayOfWeek;
            var hourMinute = $"{ today:HH: mm: ss}";
            var IdGroup = db.Agent_Employee.Where(e => e.Usuario == t.UserName).
                            Select(x => x.Id_GroupHorary).FirstOrDefault();

              var k = db.Agent_GroupHoraryDetail.Where(j => (int)j.Day == day && j.Id_GroupHorary.ToString() == IdGroup.ToString()).ToList();

            for (var i = 0; i < k.Count; i++)
            {
                var h = today.TimeOfDay;
                var Hourinitial = k[i].HourFrom.TimeOfDay;
                var HourFinish = k[i].HourUntil.TimeOfDay;

                //if (h >= Hourinitial && h <= HourFinish) 
                //{
                //    return Request.CreateResponse(HttpStatusCode.OK, true); ;
                //}
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }

            return Request.CreateResponse(HttpStatusCode.OK, false); ;
            }
            //else
            //{
            //    return ut.ReturnResponse(t);
            //}
    }
}
