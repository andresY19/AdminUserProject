using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using Queue.Action.Mapper;
using Queue.Controllers.Token;
using Queue.DAL;
using Queue.Models;
using Queue.Models.ResponseDTO;
using static Queue.Common.CommonEnum;

namespace Queue.Action
{
    public class aSecurity : AutoMapperBase
    {
        aUtilities autil = new aUtilities();
        ClaimsPrincipal cp = new ClaimsPrincipal();
        TokenValidationHandler tvh = new TokenValidationHandler();
        #region Login

        public object Login(object key)
        {
            ///cambios del dia 23/03/2019
            using (QueueContext ent = new QueueContext())
            {
                Response rp = new Response();
                try
                {
                    Agent_Empresa ae = ent.Agent_Empresa.Where(a=> a.Key == key && a.status == true).SingleOrDefault();

                    if (ae != null)
                    {
                        var token = tvh.GenerateToken(ae.Nombre, ae.Rut, ae.IdCompany.ToString());


                        ResponseConfigurationDTO responseConfigurationDTO = (from c in ent.Agent_Configuration
                                                                             where c.Agent_Empresa.IdCompany == ae.IdCompany
                                                                             select new ResponseConfigurationDTO
                                                                             {
                                                                                 Id_Configuration = c.Id_Configuration,
                                                                                 InactivityPeriod = c.InactivityPeriod,
                                                                                 UploadFrecuency = c.UploadFrecuency,
                                                                                 CaptureFrecuency = c.CaptureFrecuency,
                                                                                 token = token,
                                                                                 IsLogged = true
                                                                             }).FirstOrDefault();
                        
                        rp.data = responseConfigurationDTO;
                    }
                    else
                    {
                        //login invalido                
                        return autil.ReturnMesagge(ref rp, (int)GenericErrors.ErrorLogin, string.Empty, null); ;
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
        #endregion
    }
}