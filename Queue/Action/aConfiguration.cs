using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using Queue.DAL;
using Queue.Action.Mapper;
using Queue.Controllers.Token;
using Queue.Models;
using Queue.Models.ResponseDTO;
using static Queue.Common.CommonEnum;

namespace Queue.Action
{
    public class aConfiguration : AutoMapperBase
    {
        aUtilities autil = new aUtilities();
        ClaimsPrincipal cp = new ClaimsPrincipal();
        TokenValidationHandler tvh = new TokenValidationHandler();

        public object GetConfiguration(ConfigurationModel model)
        {
            Response rp = new Response();
            try
            {
                using (QueueContext ent = new QueueContext())
                {
                    var agent_Configuration = ent.Agent_Configuration.Where(r => r.Agent_Empresa.IdCompany == model.Id_Empresa).FirstOrDefault();
                    rp.data = _mapper.Map<ResponseConfigurationDTO>(agent_Configuration);
                }
                //retorna un response, con el campo data lleno con la respuesta.               
                return autil.ReturnMesagge(ref rp, (int)GenericErrors.OK, null, null, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                //error general               
                return autil.ReturnMesagge(ref rp, (int)GenericErrors.GeneralError, string.Empty, null, HttpStatusCode.InternalServerError);
            }
        }
    }
}