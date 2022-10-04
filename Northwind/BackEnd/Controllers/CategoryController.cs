﻿using BackEnd.DAL;
using BackEnd.Models;
using DAL;
using Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private ICategoryDAL categoryDAL;

        public CategoryController()
        {
            categoryDAL = new CategoryDALImpl(new NorthWindContext());
        }

        CategoryModel Convertir(Category category)
        {

            return new CategoryModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description
                //,                Picture= category.Picture
            };

        }

        Category Convertir(CategoryModel category)
        {

            return new Category
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description
                //  ,                Picture = category.Picture
            };

        }







        #region Consultar
        // GET: api/<CategoryController>
        [HttpGet]
        public JsonResult Get()
        {


            IEnumerable<Category> categories;
            categories = categoryDAL.GetAll();

            List<CategoryModel> result = new List<CategoryModel>();
            foreach (Category category in categories)
            {
                result.Add(Convertir(category));
            }
         

            return new JsonResult(result);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}", Name = "Get")]
        public JsonResult Get(int id)
        {

            Category category;
            category = categoryDAL.Get(id);

            return new JsonResult(Convertir(category));
        }
        #endregion

        #region Agregar
        // POST api/<CategoryController>
        [HttpPost]
        public JsonResult Post([FromBody] Category category)
        {

            try
            {
                categoryDAL.Add(category);
                return new JsonResult(Convertir(category));

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Actualizar

        // PUT api/<CategoryController>/5
        [HttpPut]
        public JsonResult Put([FromBody] Category category)
        {
            try
            {
                categoryDAL.Update(category);
                return new JsonResult(category);

            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion


        #region Eliminar
        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            try
            {

                Category category = new Category { CategoryId = id };
                //category = categoryDAL.Get(id);

                categoryDAL.Remove(category);
                return new JsonResult(category);


            }
            catch (Exception)
            {

                throw;
            }


        }
        #endregion
    }
}
