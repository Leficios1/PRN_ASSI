using BO.Models;
using DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class JewelryRepo : IJewelryRepo
    {
        private readonly SilverJewelry2023DbContext _context;

        public JewelryRepo(SilverJewelry2023DbContext context)
        {
            _context = context;
        }

        public async Task<bool> addJewelry(SilverJewelry silverJewelry)
        {
            try
            {
                var data = await _context.SilverJewelries.AddAsync(silverJewelry);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> deleteJewelry(string jewlryid)
        {
            var data = await _context.SilverJewelries.Where(x => x.SilverJewelryId == jewlryid).SingleOrDefaultAsync();
            if (data != null)
            {
                _context.SilverJewelries.Remove(data);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<SilverJewelry>> getAll()
        {
            try
            {
                var data = await _context.SilverJewelries.Include(x => x.Category).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SilverJewelry> getById(string id)
        {
            var data = await _context.SilverJewelries.Where(x => x.SilverJewelryId == id).Include(x => x.Category).SingleOrDefaultAsync();
            return data;
        }

        public async Task<bool> updateJewelry(SilverJewelry silverJewelry)
        {
            var data = await _context.SilverJewelries.Where(x => x.SilverJewelryId == silverJewelry.SilverJewelryId).SingleOrDefaultAsync();
            if (data != null)
            {
                // Detach existing entity
                _context.Entry(data).State = EntityState.Detached;

                // Attach new entity and mark as modified
                _context.SilverJewelries.Attach(silverJewelry);
                _context.Entry(silverJewelry).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
