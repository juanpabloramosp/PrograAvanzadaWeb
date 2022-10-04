using FrontEnd.Helpers;
using FrontEnd.Models;
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
                List<CategoryViewModel> categories = 
                    JsonConvert.DeserializeObject<List<CategoryViewModel>>(content);

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
            CategoryViewModel categoryViewModel = response.Content.ReadAsAsync<CategoryViewModel>().Result;
            //ViewBag.Title = "All Products";
            return View(categoryViewModel);
        }
        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel category)
        {
            try
            {



                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.PostResponse("api/category", category);
                response.EnsureSuccessStatusCode();
                CategoryViewModel categoryViewModel = response.Content.ReadAsAsync<CategoryViewModel>().Result;
                return RedirectToAction("Details", new { id = categoryViewModel.CategoryID });
            }
            catch (HttpRequestException
          )
            {
                return RedirectToAction("Error", "Home");
            }

            catch (Exception
            )
            {

                throw;
            }
        }


        public ActionResult Edit(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/category/" + id.ToString());
            response.EnsureSuccessStatusCode();
            CategoryViewModel categoryViewModel = response.Content.ReadAsAsync<CategoryViewModel>().Result;
            //ViewBag.Title = "All Products";
            return View(categoryViewModel);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        public ActionResult Edit(CategoryViewModel category)
        {


            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.PutResponse("api/category", category);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Details", new { id = category.CategoryID });
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {


            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/category/" + id.ToString());
            response.EnsureSuccessStatusCode();
            CategoryViewModel categoryViewModel = response.Content.ReadAsAsync<CategoryViewModel>().Result;
            //ViewBag.Title = "All Products";
            return View(categoryViewModel);
        }


        [HttpPost]
        public ActionResult Delete(CategoryViewModel category)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.DeleteResponse("api/category/" + category.CategoryID.ToString());
            response.EnsureSuccessStatusCode();
            bool Eliminado = response.Content.ReadAsAsync<bool>().Result;

            if (Eliminado)
            {
                return RedirectToAction("Index");
            }
            else
            {
                throw new Exception();
            }
        }

    }
}
