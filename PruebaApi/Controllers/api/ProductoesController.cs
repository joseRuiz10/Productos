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
    public class ProductoesController : ApiController
    {
        private Database1Entities db = new Database1Entities();

        // GET: api/Productoes
        public IHttpActionResult GetProducto()
        {
            IList<ProductoViewModel> productos = null;

            productos = db.Producto.Include("Color").Include("Talle")
                .Select(s => new ProductoViewModel()
                {
                    IdProducto = s.IdProducto,
                    Tipo = s.Tipo,

                    Color = s.Color == null ? null : new ColorViewModel()
                    {
                        IdColor = s.Color.IdColor,
                        color = s.Color.color1
                     
                    },
                      Talle = s.Talle == null ? null : new TalleViewModel()
                      {
                          IdTalle = s.Talle.IdTalle,
                          Talle1 = s.Talle.talle1

                      }
                }).ToList<ProductoViewModel>();


            return Ok( productos);
        }

        

        // GET: api/Productoes/5
        [ResponseType(typeof(Producto))]
        public IHttpActionResult GetProducto(int id)
        {
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // PUT: api/Productoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProducto(int id, Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != producto.IdProducto)
            {
                return BadRequest();
            }

            db.Entry(producto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
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

        // POST: api/Productoes
        [ResponseType(typeof(Producto))]
        public IHttpActionResult PostProducto(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Producto.Add(new Producto()
            {

                Tipo= producto.Tipo
                

            });

            db.SaveChanges();

            return Ok();
        }

        // DELETE: api/Productoes/5
        [ResponseType(typeof(Producto))]
        public IHttpActionResult DeleteProducto(int id)
        {
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return NotFound();
            }

            db.Producto.Remove(producto);
            db.SaveChanges();

            return Ok(producto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductoExists(int id)
        {
            return db.Producto.Count(e => e.IdProducto == id) > 0;
        }
    }
}