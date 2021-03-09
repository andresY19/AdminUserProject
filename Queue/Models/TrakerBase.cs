using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Queue.Models
{
    public class TrakerBase
    {
        public BsonBinaryData _id { get; set; } = new BsonBinaryData(Guid.NewGuid(), GuidRepresentation.Standard);
        public string Application { get; set; }
        public string Title { get; set; }
        public Nullable<double> Time { get; set; }
        public Nullable<double> Inactivity { get; set; }
        public string Ip { get; set; }
        public string Pc { get; set; }
        public string UserName { get; set; }
        public double Activity { get; set; }
        public DateTime Date { get; set; }
        public string IdEmpresa { get; set; }
    }
}