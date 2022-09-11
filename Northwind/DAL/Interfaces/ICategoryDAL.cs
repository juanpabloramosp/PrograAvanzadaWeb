using BackEnd.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public interface ICategoryDAL: IDALGenerico<Category>
    {
        List<Category> GetByName(string Name);
        List<Category> GetByNameSP(string Name);
    }
}
