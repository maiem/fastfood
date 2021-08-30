using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FASTFOOD.Controllers
{
    [EnableCorsAttribute("*", "*", "GET,POST,PUT,DELETE")]
    public class DanhMucController : ApiController
    {
        //[DisableCors]
        public HttpResponseMessage Get()
        {
            try
            {

                using(CSDL_FASTFOOD_DBContext entity =  new CSDL_FASTFOOD_DBContext())
                {
                    var dmList = entity.DANHMUCs.ToList();
                    var response = new List<DANHMUC>();
                    foreach (var dm in dmList)
                    {
                        response.Add(new DANHMUC()
                        {
                            MaDM = dm.MaDM,
                            TenDM = dm.TenDM,
                            Mota = dm.Mota
                        });
                    }
                    if (dmList != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Không tìm thấy danh mục sản phẩm");
                    }
                }
            }
            catch(Exception e) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }


        // lấy 1 danh mục sản phẩm theo id
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                using (CSDL_FASTFOOD_DBContext entity = new CSDL_FASTFOOD_DBContext())
                {

                    var temp = entity.DANHMUCs.FirstOrDefault(dm => dm.MaDM == id);
                    DANHMUC dmResponse = new DANHMUC()
                    {
                        MaDM = temp.MaDM,
                        TenDM = temp.TenDM,
                        Mota = temp.Mota
                    };

                    if (temp != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, dmResponse);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Danh muc co ma" + id + " khong ton tai");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        public HttpResponseMessage Post([FromBody] DANHMUC dm)
        {
            try
            {
                using (CSDL_FASTFOOD_DBContext entity = new CSDL_FASTFOOD_DBContext())
                {
                    entity.DANHMUCs.Add(dm);
                    entity.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, dm);
                    message.Headers.Location = new Uri(Request.RequestUri + dm.MaDM.ToString());
                    return message;
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }


        // sua danh muc Sp
        public HttpResponseMessage Put(DANHMUC dm, int id)
        {
            try
            {
                using (CSDL_FASTFOOD_DBContext entity = new CSDL_FASTFOOD_DBContext())
                {
                    var temp = entity.DANHMUCs.FirstOrDefault(e => e.MaDM == id);
                    DANHMUC dmResponse = new DANHMUC()
                    {
                        MaDM = temp.MaDM,
                        TenDM = temp.TenDM,
                        Mota = temp.Mota
                    };

                    if (temp != null)
                    {
                        id = dm.MaDM;
                        temp.TenDM = dm.TenDM;
                        temp.Mota = dm.Mota;

                        entity.SaveChanges();

                        var mess = Request.CreateResponse(HttpStatusCode.OK, dm);
                        mess.Headers.Location = new Uri(Request.RequestUri + dm.MaDM.ToString());

                        return mess;
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Danh muc co ma" + id + " khong ton tai");
                    }


                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (CSDL_FASTFOOD_DBContext entity = new CSDL_FASTFOOD_DBContext())
                {
                    var temp = entity.DANHMUCs.FirstOrDefault(e => e.MaDM == id);
                    DANHMUC dmResponse = new DANHMUC()
                    {
                        MaDM = temp.MaDM,
                        TenDM = temp.TenDM,
                        Mota = temp.Mota
                    };

                    if (temp != null && entity.SANPHAMs.Count() == 0)
                    {
                        entity.DANHMUCs.Remove(temp);
                        entity.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else if (temp != null && entity.SANPHAMs.Count() > 0)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Danh muc chua san pham.\n Khong duoc xoa danh muc nay");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Danh muc khong ton tai");
                    }


                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}
