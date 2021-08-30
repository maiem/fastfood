using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Security.Principal;

namespace FASTFOOD.Security
{
    public class XacThucAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)// Kiem tra header
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);// Khong cho phep vi khong co header
            }
            else
            {
                string xacThucToken = actionContext.Request.Headers.Authorization.Parameter;
                // token xac thuc khi duoc gui len server se co dang nhu sau:  email:matkhau
                // va duoc ma hoa bang base64
                // vd:      admin:123
                // =>       YWRtaW46MTIz
                // https://kt.gy/tools.html#conv/admin%3A123 (click de xem them, dong B64)

                string decodedXacThucToken = Encoding.UTF8.GetString(Convert.FromBase64String(xacThucToken));// giai ma base64 ve string binh thuong
                string[] thongTinDangNhapArray = decodedXacThucToken.Split(':');
                string email = thongTinDangNhapArray[0];
                string matKhau = thongTinDangNhapArray[1];

                if (NguoiDungSecurity.DangNhap(email, matKhau))
                { // Dang nhap thanh cong!
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(email), null);
                }
                else
                { // Dang nhap that bai!
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}