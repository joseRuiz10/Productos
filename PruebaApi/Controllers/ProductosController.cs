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



        public ActionResult Edit(int id)
        {
           ProductoViewModel producto = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44353/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Productoes?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ProductoViewModel>();
                    readTask.Wait();

                    producto = readTask.Result;
                }
            }

            return View(producto);
        }


       [HttpPost]
    public ActionResult Edit(ProductoViewModel producto)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44353/api/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<ProductoViewModel>("Productoes", producto);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(producto);
        }

         public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44353/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Productoes/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}
