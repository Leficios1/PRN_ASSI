using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IJewelryRepo
    {
        public Task<List<SilverJewelry>> getAll();
        public Task<SilverJewelry> getById(string id);
        public Task<bool> addJewelry(SilverJewelry silverJewelry);
        public Task<bool> updateJewelry(SilverJewelry silverJewelry);
        public Task<bool> deleteJewelry(string jewlryid);
    }
}
