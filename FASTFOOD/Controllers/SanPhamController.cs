using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using FASTFOOD;

namespace FASTFOOD.Controllers
{
    [EnableCorsAttribute("*", "*", "GET,POST,PUT,DELETE")]
    public class SanPhamController : ApiController
    {
        private CSDL_FASTFOOD_DBContext db = new CSDL_FASTFOOD_DBContext();

        // GET: api/SanPham
        public IQueryable<SANPHAM> GetSANPHAMs()
        {
            return db.SANPHAMs;
        }

        // GET: api/SanPham/5
        [ResponseType(typeof(SANPHAM))]
        public IHttpActionResult GetSANPHAM(int id)
        {
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return NotFound();
            }

            return Ok(sANPHAM);
        }

        // PUT: api/SanPham/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSANPHAM(int id, SANPHAM sANPHAM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sANPHAM.MaSP)
            {
                return BadRequest();
            }

            db.Entry(sANPHAM).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SANPHAMExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SanPham
        [ResponseType(typeof(SANPHAM))]
        public IHttpActionResult PostSANPHAM([FromBody]SANPHAM sANPHAM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SANPHAMs.Add(sANPHAM);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sANPHAM.MaSP }, sANPHAM);
        }

        // DELETE: api/SanPham/5
        [ResponseType(typeof(SANPHAM))]
        public IHttpActionResult DeleteSANPHAM(int id)
        {
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return NotFound();
            }

            db.SANPHAMs.Remove(sANPHAM);
            db.SaveChanges();

            return Ok(sANPHAM);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SANPHAMExists(int id)
        {
            return db.SANPHAMs.Count(e => e.MaSP == id) > 0;
        }
    }
}