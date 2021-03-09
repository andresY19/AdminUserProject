using Queue.Action;
using System.Net.Http;
using System.Web.Http;

namespace Queue.Controllers
{
    [RoutePrefix("api/Security")]
    public class SecurityController : ApiController
    {
        aUtilities ut = new aUtilities();
        #region Login
        /// <summary>
        /// Metodo para loguear y traer la lista de entidades
        /// </summary>
        /// <remarks>
        /// PARAMETRO: user [STRING] <br />
        /// PARAMETRO: password [STRING]
        /// </remarks>
        /// <returns>Regresa un usuario con la lista de entidades a las que pertenece </returns>
        [HttpPost]
        [Route("Login")]
        public HttpResponseMessage Login(object key)
        {
            aSecurity s = new aSecurity();
            return ut.ReturnResponse(s.Login(key));
        }

        //[HttpGet]
        //[Route("Validate")]
        //public HttpResponseMessage Validate()
        //{
        //    return ut.ReturnResponse("Activa");
        //}
        #endregion
    }
}
