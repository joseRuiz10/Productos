using System;
using PruebaApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;

namespace PruebaApi.Controllers
{
    public class ColoresController : Controller
    {
        // GET: Colors
        public ActionResult Index()
        {
            IEnumerable<ColorViewModel> colores = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44353/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Colors");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ColorViewModel>>();
                    readTask.Wait();

                    colores = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    colores = Enumerable.Empty<ColorViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(colores);
        }
        public ActionResult Create()
        {

            return View();

        }

        [HttpPost]
        public ActionResult Create(ColorViewModel color)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44353/api/Colors");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ColorViewModel>("Colors", color);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(color);
        }

    }
}