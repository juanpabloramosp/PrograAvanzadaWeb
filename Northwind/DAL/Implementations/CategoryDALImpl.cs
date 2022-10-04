using DAL;
using DAL.Implementations;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class CategoryDALImpl : ICategoryDAL
    {
        NorthWindContext context;
       

        public CategoryDALImpl()
        {
            context = new NorthWindContext();

        }

        public CategoryDALImpl(NorthWindContext northWindContext)
        {
            this.context = northWindContext;
        }

        public bool Add(Category entity)
        {
            try
            {
                //sumar o calcular 


                //string sql = "[dbo].[sp_add_Category] @CategoryName, @Description";
                //var param = new SqlParameter[] {
                //        new SqlParameter() {
                //            ParameterName = "@CategoryName",
                //            SqlDbType =  System.Data.SqlDbType.VarChar,
                //            Size = 10,
                //            Direction = System.Data.ParameterDirection.Input,
                //            Value = entity.CategoryName
                //        },
                //          new SqlParameter() {
                //            ParameterName = "@Description",
                //            SqlDbType =  System.Data.SqlDbType.VarChar,
                //            Size = 10,
                //            Direction = System.Data.ParameterDirection.Input,
                //            Value = entity.Description
                //        }

                //};

                //context.Database.ExecuteSqlRaw(sql, param);

                //return true;

                using (UnidadDeTrabajo<Category> unidad = new UnidadDeTrabajo<Category>(context))
                {
                    unidad.genericDAL.Add(entity);
                    return unidad.Complete();
                }

            }
            catch (Exception)
            {

                return false;
            }
        }

        public void AddRange(IEnumerable<Category> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> Find(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Category Get(int CategoryId)
        {
            try
            {
                Category category;
                using (UnidadDeTrabajo<Category> unidad = new UnidadDeTrabajo<Category>(context))
                {
                    category = unidad.genericDAL.Get(CategoryId);
                }
                return category;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Category> Get()
        {
            try
            {
                IEnumerable<Category> categories;
                using (UnidadDeTrabajo<Category> unidad = new UnidadDeTrabajo<Category>(context))
                {
                    categories = unidad.genericDAL.GetAll();
                }
                return categories.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Category> GetAll()
        {
            try
            {
                IEnumerable<Category> categories;
                using (UnidadDeTrabajo<Category> unidad = new UnidadDeTrabajo<Category>(context))
                {
                    categories = unidad.genericDAL.GetAll();
                }
                return categories;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<Category> GetByName(string Name)
        {
            List<Category> lista;

            using (context = new NorthWindContext())
            {
                lista = (from c in context.Categories
                         where c.CategoryName.Contains(Name)
                         select c).ToList();
            }
            return lista;

        }


        public List<Category> GetByNameSP(string Name)
        {


            List<sp_GetCategoriesByName_Result> results;
            string sql = "[dbo].[sp_GetCategoriesByName] @Name";
            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@Name",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 10,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = Name
                        } };

            results = context.sp_GetCategoriesByName_Results.FromSqlRaw(sql, param)
                    .ToListAsync()
                    .Result;




            List<Category> categories = new List<Category>();
            foreach (var item in results)
            {
                categories.Add(
                    new Category
                    {
                        CategoryId = item.CategoryId,
                        CategoryName = item.CategoryName,
                        Description = item.Description,
                        Picture = item.Picture
                    });
            }

            return categories;
        }



        public bool Remove(Category entity)
        {
            bool result = false;
            try
            {
                using (UnidadDeTrabajo<Category> unidad = new UnidadDeTrabajo<Category>(context))
                {
                    unidad.genericDAL.Remove(entity);
                    result = unidad.Complete();
                }

            }
            catch (Exception)
            {

                result = false;
            }

            return result;
        }

        public void RemoveRange(IEnumerable<Category> entities)
        {
            throw new NotImplementedException();
        }

        public Category SingleOrDefault(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Update(Category category)
        {
            bool result = false;

            try
            {
                using (UnidadDeTrabajo<Category> unidad = new UnidadDeTrabajo<Category>(context))
                {
                    unidad.genericDAL.Update(category);
                    result = unidad.Complete();
                }

            }
            catch (Exception)
            {

                return false;
            }

            return result;
        }

      
    }
}
