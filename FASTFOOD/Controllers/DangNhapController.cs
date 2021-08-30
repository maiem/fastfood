using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Threading;
using FASTFOOD.Security;

namespace FASTFOOD.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class DangNhapController : ApiController
    {
        [XacThuc]
        public HttpResponseMessage Get(string email = "")
        {
            string diaChiEmail = Thread.CurrentPrincipal.Identity.Name;
            /*if(email == "")
                return Request.CreateResponse(HttpStatusCode.BadRequest, "???");*/
            using (CSDL_FASTFOOD_DBContext entities = new CSDL_FASTFOOD_DBContext())
            {
                USER user = entities.USERS.Where(e => e.Email == diaChiEmail).First();
                int maQuyen = (int)user.MaQuyen;
                switch (maQuyen)
                {
                    case 1: // admin
                        return Request.CreateResponse(HttpStatusCode.OK, "admin");
                    case 2: // user
                        return Request.CreateResponse(HttpStatusCode.OK, "user");
                    default:
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "sai mat khau");
                }
            }
        }
    }
}
