using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ShopBridge.Controllers
{
    public class ProductController : Controller
    {
        HttpClient client = new HttpClient();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Display()
        {
            List<Product> list = new List<Product>();
            client.BaseAddress = new Uri("http://localhost:61809/api/webapi/GetData");
            var consume = client.GetAsync("GetData");
            consume.Wait();
            var test = consume.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Product>>();
                list = display.Result;
            }
            return View(list);
        }

        [HttpGet]
        public ActionResult InsertData()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertData(Product data)
        {
            client.BaseAddress = new Uri("http://localhost:61809/api/webapi/InsertData");
            var consume = client.PostAsJsonAsync("Insert",data);
            consume.Wait();
            var test = consume.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Display");
            }
            else {
                return HttpNotFound();
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Product product = new Product();
            client.BaseAddress = new Uri("http://localhost:61809/api/webapi/Details");
            var consume = client.GetAsync("Details?id="+ id.ToString());
            consume.Wait();
            var test = consume.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Product>();
                product = display.Result;
            }
            return View(product);
        }

        public ActionResult Delete(int id)
        {
            Product product = new Product();
            client.BaseAddress = new Uri("http://localhost:61809/api/webapi/Delete");
            var consume = client.DeleteAsync("Delete?id=" + id.ToString());
            consume.Wait();
            var test = consume.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Display");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Product product = null;
            client.BaseAddress = new Uri("http://localhost:61809/api/webapi/Details");
            var consume = client.GetAsync("Details?id=" + id.ToString());
            consume.Wait();
            var test = consume.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Product>();
                display.Wait();
                product = display.Result;
            }
                return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product data)
        {
            client.BaseAddress = new Uri("http://localhost:61809/api/webapi/Update");
            var consume = client.PutAsJsonAsync<Product>("Update",data);
            consume.Wait();
            var test = consume.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Display");
            }
            return View();
        }
    }
}