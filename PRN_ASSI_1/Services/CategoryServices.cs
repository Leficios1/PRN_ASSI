using BO.Models;
using Repository;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepo repo;

        public CategoryServices(ICategoryRepo repo)
        {
            this.repo = repo;
        }

        public List<Category> GetCategories()
        {
            return repo.GetCategories();
        }

        public Category GetCategories(string id)
        {
            return repo.GetCategory(id);
        }
    }
}
