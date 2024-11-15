using BOs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class SilverJewelryDAO
    {
        private static SilverJewelryDAO instance;
        private readonly SilverJewelry2023DbContext context;

        private SilverJewelryDAO()
        {
            context = new SilverJewelry2023DbContext();
        }

        public static SilverJewelryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SilverJewelryDAO();
                }
                return instance;
            }
        }
        public async Task<SilverJewelry> getById(string id)
        {
            return await context.SilverJewelries.Where(x => x.SilverJewelryId == id).SingleOrDefaultAsync();
        }
        public async Task<List<SilverJewelry>> getAll()
        {
            return await context.SilverJewelries.ToListAsync();
        }
        public async Task<SilverJewelry> create(SilverJewelry dto)
        {
            // Kiểm tra dto null
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            // Kiểm tra trùng ID
            var existingJewelry = await context.SilverJewelries
                .FirstOrDefaultAsync(x => x.SilverJewelryId == dto.SilverJewelryId);

            if (existingJewelry != null)
            {
                throw new Exception($"SilverJewelry with ID {dto.SilverJewelryId} already exists");
            }

            // Validation dữ liệu
            if (string.IsNullOrEmpty(dto.SilverJewelryName))
            {
                throw new ArgumentException("SilverJewelryName is required");
            }

            if (dto.Price <= 0)
            {
                throw new ArgumentException("Price must be greater than 0");
            }

            if (dto.ProductionYear < 1900)
            {
                throw new ArgumentException("ProductionYear must be >= 1900");
            }

            // Set giá trị mặc định cho CreatedDate
            dto.CreatedDate = DateTime.Now;

            try
            {
                // Thêm và lưu vào database
                var result = await context.SilverJewelries.AddAsync(dto);
                await context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating SilverJewelry: {ex.Message}");
            }
        }
        public async Task<SilverJewelry> update(SilverJewelry dto)
        {
            var data = await context.SilverJewelries.Where(x => x.SilverJewelryId == dto.SilverJewelryId).SingleOrDefaultAsync();
            if (data == null)
            {
                throw new Exception("Not Found User");
            }
            data.SilverJewelryName = dto.SilverJewelryName;
            data.SilverJewelryDescription = dto.SilverJewelryDescription;
            data.MetalWeight = dto.MetalWeight;
            data.Price = dto.Price;
            data.ProductionYear = dto.ProductionYear;
            data.CreatedDate = dto.CreatedDate;
            data.CategoryId = dto.CategoryId;

            context.SilverJewelries.Update(data);
            await context.SaveChangesAsync();
            return dto;
        }
        public async Task<List<Category>>getCategoryBysilverJwery(string silverId)
        {
            return await context.Categories.ToListAsync();
        }
        public async Task<List<Category>> getAllCategory()
        {
            return await context.Categories.ToListAsync();
        }
        public async Task<bool> deleted(string silverid)
        {
            try
            {
                // Kiểm tra dữ liệu tồn tại
                var data = await context.SilverJewelries
                    .FirstOrDefaultAsync(x => x.SilverJewelryId == silverid);

                if (data == null)
                {
                    throw new Exception($"SilverJewelry with ID {silverid} not found");
                }

                // Xóa dữ liệu
                context.SilverJewelries.Remove(data);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Log lỗi
                Console.WriteLine($"Error deleting SilverJewelry: {ex.Message}");
                throw; // Throw lại exception để controller xử lý
            }
        }
    }
}
