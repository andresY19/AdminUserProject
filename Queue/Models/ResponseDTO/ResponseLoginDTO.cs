using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Queue.Models.ResponseDTO
{
    public class ResponseLoginDTO
    {
        public string token { get; set; }
        public bool IsLogged { get; set; }
        public Nullable<System.Guid> Id_Empresa { get; set; }
    }
}