using PruebaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PruebaApi.Controllers
{
    public class TallersController : Controller
    {
        // GET: Tallers
        public ActionResult Index()
        {
            IEnumerable<TalleViewModel> talles = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44353/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Talles");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<TalleViewModel>>();
                    readTask.Wait();

                    talles = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    talles = Enumerable.Empty<TalleViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(talles);
        }
        public ActionResult Create()
        {

            return View();

        }

        [HttpPost]
        public ActionResult Create(TalleViewModel talle)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44353/api/Tallers");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<TalleViewModel>("Talles", talle);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(talle);
        }


        
    public ActionResult Edit(int id)
    {
        TalleViewModel talle = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://localhost:44353/api/");
            //HTTP GET
            var responseTask = client.GetAsync("Talles?id=" + id.ToString());
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<TalleViewModel>();
                readTask.Wait();

                talle = readTask.Result;
            }
        }
        return View(talle);
    }

    [HttpPost]
    public ActionResult Edit(TalleViewModel talle)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://localhost:44353/api/talle");

            //HTTP POST
            var putTask = client.PutAsJsonAsync<TalleViewModel>("talle", talle);
            putTask.Wait();

            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }
        }
        return View(talle);
    }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44353/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Talles/" + id.ToString());
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