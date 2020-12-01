using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PruebaApi;
using PruebaApi.Models;

namespace PruebaApi.Controllers.api
{
    public class TallesController : ApiController
    {
        private Database1Entities db = new Database1Entities();

        // GET: api/Talles
        public IHttpActionResult GetTalles()
        {
            IList<TalleViewModel> talles = null;

            talles = db.Talle
                .Select(s => new TalleViewModel()
                {
                    IdTalle = s.IdTalle,
                    Talle1 = s.talle1


                }).ToList<TalleViewModel>();


            return Ok(talles);
        }

        // GET: api/Talles/5
        [ResponseType(typeof(Talle))]
        public IHttpActionResult GetTalle(int id)
        {
            Talle talle = db.Talle.Find(id);
            if (talle == null)
            {
                return NotFound();
            }

            return Ok(talle);
        }

        // PUT: api/Colors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutColor(int id, Talle talle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != talle.IdTalle)
            {
                return BadRequest();
            }

            db.Entry(talle).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TalleExists(id))
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

        // POST: api/Talles
        [ResponseType(typeof(Talle))]
        public IHttpActionResult PostColor(Talle talle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Talle.Add(new Talle()
            {

                talle1 = talle.talle1


            });

            db.SaveChanges();

            return Ok();
        }

        // DELETE: api/Colors/5
        [ResponseType(typeof(Talle))]
        public IHttpActionResult DeleteTalle(int id)
        {
            Talle talle = db.Talle.Find(id);
            if (talle == null)
            {
                return NotFound();
            }

            db.Talle.Remove(talle);
            db.SaveChanges();

            return Ok(talle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TalleExists(int id)
        {
            return db.Talle.Count(e => e.IdTalle == id) > 0;
        }
    }
}