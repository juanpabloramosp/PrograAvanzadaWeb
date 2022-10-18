using DAL.Implementations;
using DAL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private ISupplierDAL supplierDAL;

        public SupplierController()
        {
            supplierDAL = new SupplierDALImpl(NorthWindContext.GetInstance());
        }


        // GET: api/<SupplierController>
        [HttpGet]
        public JsonResult Get()
        {
            List<Supplier> suppliers;    
            suppliers=supplierDAL.GetAll().ToList();

            return new JsonResult(suppliers);
        }

        // GET api/<SupplierController>/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            Supplier supplier;
            supplier = supplierDAL.Get(id);
            return new JsonResult(supplier);

        }

        // POST api/<SupplierController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SupplierController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SupplierController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
