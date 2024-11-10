using BOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ISilverJewelryRepo
    {
        Task<SilverJewelry> getById(string id);
        Task<List<SilverJewelry>> getAll();
        Task<List<Category>> getAllCategory();
        Task<SilverJewelry> create(SilverJewelry dto);
        Task<SilverJewelry> update(SilverJewelry dto);
        Task<bool> deleted(string id);
    }
}
