using BOs.Models;
using DAO;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SilverJewelryRepo : ISilverJewelryRepo
    {
        public async Task<SilverJewelry> create(SilverJewelry dto)
        {
            return await SilverJewelryDAO.Instance.create(dto);
        }

        public async Task<bool> deleted(string id)
        {
            return await SilverJewelryDAO.Instance.deleted(id);
        }

        public async Task<List<SilverJewelry>> getAll()
        {
            return await SilverJewelryDAO.Instance.getAll();
        }

        public async Task<SilverJewelry> getById(string id)
        {
            return await SilverJewelryDAO.Instance.getById(id);
        }

        public async Task<SilverJewelry> update(SilverJewelry dto)
        {
            return await SilverJewelryDAO.Instance.update(dto);
        }

        public async Task<List<Category>> getAllCategory()
        {
            return await SilverJewelryDAO.Instance.getAllCategory();
        }
    }
}
