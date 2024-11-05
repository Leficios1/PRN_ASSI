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
    public class JewelryServices : IJewelryServices
    {
        private readonly IJewelryRepo _repo;

        public JewelryServices(IJewelryRepo repo)
        {
            _repo = repo;
        }

        public async Task<bool> create(SilverJewelry silverJewelry)
        {
            return await _repo.addJewelry(silverJewelry);
        }

        public async Task<bool> deleteById(string id)
        {
            return await _repo.deleteJewelry(id);
        }

        public async Task<List<SilverJewelry>> getAll()
        {
            return await _repo.getAll();
        }

        public async Task<SilverJewelry> getById(string id)
        {
            return await _repo.getById(id);
        }

        public async Task<bool> update(SilverJewelry dto)
        {
            return await _repo.updateJewelry(dto);
        }
    }
}
