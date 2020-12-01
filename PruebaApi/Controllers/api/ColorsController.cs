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
    public class ColorsController : ApiController
    {
        private Database1Entities db = new Database1Entities();

        // GET: api/Colors
        public IHttpActionResult GetColor()
        {
            IList<ColorViewModel> colores = null;

            colores = db.Color
                .Select(s => new ColorViewModel()
                {
                    IdColor = s.IdColor,
                    color = s.color1

                
                }).ToList<ColorViewModel>();


            return Ok(colores);
        }

        // GET: api/Colors/5
        [ResponseType(typeof(Color))]
        public IHttpActionResult GetColor(int id)
        {
            Color color = db.Color.Find(id);
            if (color == null)
            {
                return NotFound();
            }

            return Ok(color);
        }

        // PUT: api/Colors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutColor(int id, Color color)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != color.IdColor)
            {
                return BadRequest();
            }

            db.Entry(color).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorExists(id))
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

        // POST: api/Colors
        [ResponseType(typeof(Color))]
        public IHttpActionResult PostColor(Color color)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Color.Add(new Color()
            {

                color1 = color.color1


            });

            db.SaveChanges();

            return Ok();
        }

        // DELETE: api/Colors/5
        [ResponseType(typeof(Color))]
        public IHttpActionResult DeleteColor(int id)
        {
            Color color = db.Color.Find(id);
            if (color == null)
            {
                return NotFound();
            }

            db.Color.Remove(color);
            db.SaveChanges();

            return Ok(color);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ColorExists(int id)
        {
            return db.Color.Count(e => e.IdColor == id) > 0;
        }
    }
}