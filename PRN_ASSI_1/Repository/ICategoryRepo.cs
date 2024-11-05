using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICategoryRepo
    {
        public Category GetCategory(string id);

        public List<Category> GetCategories();
      
    }
}
