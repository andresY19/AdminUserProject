using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace Queue.Utils
{
    public class Utils
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
    }
}