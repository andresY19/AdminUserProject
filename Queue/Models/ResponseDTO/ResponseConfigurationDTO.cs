using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Queue.Models.ResponseDTO
{
    public class ResponseConfigurationDTO
    {
        public System.Guid Id_Configuration { get; set; }
        public Nullable<int> InactivityPeriod { get; set; }
        public Nullable<int> UploadFrecuency { get; set; }
        public Nullable<int> CaptureFrecuency { get; set; }
        public string token { get; set; }
        public bool IsLogged { get; set; }
        public Nullable<System.Guid> Id_Empresa { get; set; }
        public List<Agent_GroupHorary> GroupHoraryId { get; set; }
        public List<Agent_GroupHoraryDetail> GroupHoraryDetail { get; set; }
    }
}