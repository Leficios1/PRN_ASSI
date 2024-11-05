using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class CategoryDAO
    {
        private SilverJewelry2023DbContext context;
        private static CategoryDAO instance;

        public static CategoryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryDAO();
                }
                return instance;
            }
        }
        public CategoryDAO()
        {
            context = new SilverJewelry2023DbContext();
        }

        public Category GetCategory(String id)
        {
            return context.Categories.SingleOrDefault(m => m.CategoryId.Equals(id));
        }
        public List<Category> GetCategories() 
        {
            return context.Categories.ToList();
        }
    }
}
