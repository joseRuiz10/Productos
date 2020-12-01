using PruebaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PruebaApi.Controllers
{
    public class ProductosController : Controller
    {
        // GET: Productos
        public ActionResult Index()
        {

            IEnumerable<ProductoViewModel> productos = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44353/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Productoes");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductoViewModel>>();
                    readTask.Wait();

                    productos = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    productos = Enumerable.Empty<ProductoViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(productos);
        }

        public ActionResult Create()
        {
            
                return View();
            
        }
        [HttpPost]
        public ActionResult Create(ProductoViewModel producto)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44353/api/Produectoes");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ProductoViewModel>("Productoes", producto);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(producto);
        }
    }
}