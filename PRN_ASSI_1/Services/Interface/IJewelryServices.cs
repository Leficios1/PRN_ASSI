using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IJewelryServices
    {
        Task<List<SilverJewelry>> getAll();
        Task<SilverJewelry> getById(string id);
        Task<bool> create(SilverJewelry silverJewelry);
        Task<bool> update(SilverJewelry dto);
        Task<bool> deleteById(string id);
    }
}
