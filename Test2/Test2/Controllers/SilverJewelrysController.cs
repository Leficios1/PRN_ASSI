using BOs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository;
using Repository.Interface;

namespace Test2.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class SilverJewelrysController : ODataController
    {
        private readonly ISilverJewelryRepo _silverjewelryRepo;

        public SilverJewelrysController(ISilverJewelryRepo silverjewelryRepo)
        {
            _silverjewelryRepo = silverjewelryRepo;
        }

        [EnableQuery]        
        [Authorize("Admin")]
        public async Task<IActionResult> Get()
        {
            return Ok( await _silverjewelryRepo.getAll());
        }

        [HttpGet("getById")]
        public async Task<IActionResult> getById(string id)
        {
            return Ok( await _silverjewelryRepo.getById(id));
        }
        [HttpGet("getAllCategory")]
        public async Task<IActionResult> getAllCategory()
        {
            return Ok( await _silverjewelryRepo.getAllCategory());
        } 
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] SilverJewelry dto)
        {
            return Ok( await _silverjewelryRepo.create(dto));
        }
        [HttpPut("update")]
        public async Task<IActionResult> update([FromBody] SilverJewelry dto)
        {
            return Ok( await _silverjewelryRepo.update(dto));
        }
        [HttpDelete("deleted/{id}")]
        public async Task<IActionResult> deleted([FromRoute] string id)
        {
            try
            {
                var result = await _silverjewelryRepo.deleted(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
