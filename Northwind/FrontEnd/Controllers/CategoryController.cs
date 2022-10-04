using FrontEnd.Helpers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClienteAPI.Controllers
{
    public class CategoryController : Controller
    {
        // GET: CategoryController
        public ActionResult Index()
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse("api/category/");
                response.EnsureSuccessStatusCode();
                //List<Models.CategoryViewModel> categories = new List<Models.CategoryViewModel>();
                var content = response.Content.ReadAsStringAsync().Result;
                List<Models.CategoryViewModel> categories = 
                    JsonConvert.DeserializeObject<List<Models.CategoryViewModel>>(content);

                ViewBag.Title = "All Categories";
                return View(categories);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: CategoryController/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
         

            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/category/" + id.ToString());
            response.EnsureSuccessStatusCode();
            Models.CategoryViewModel categoryViewModel = response.Content.ReadAsAsync<Models.CategoryViewModel>().Result;
            //ViewBag.Title = "All Products";
            return View(categoryViewModel);
        }



    }
}
