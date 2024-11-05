using BO.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace PRN_ASSI_1.Controllers
{
    [Authorize(Roles = "1")]
    [Route("api/[controller]")]
    [ApiController]
    public class JwerlyController : ControllerBase
    {
        private readonly IJewelryServices JewelryServices;

        public JwerlyController(IJewelryServices jewelryServices)
        {
            JewelryServices = jewelryServices;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await JewelryServices.getAll();
            if (response == null)
            {
                return Unauthorized("You don't have permission to see this");
            }
            return Ok(response);
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute]string id)
        {
            var response = await JewelryServices.getById(id);
            if (response == null)
            {
                return Unauthorized("You don't have permission to see this");
            }
            return Ok(response);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]SilverJewelry dto)
        {
            var response = await JewelryServices.create(dto);
            if (response == false)
            {
                return Unauthorized("Can't create");
            }
            return Ok(response);
        }
        [HttpPut("update")]
        public async Task<IActionResult> update([FromBody]SilverJewelry dto)
        {
            var response = await JewelryServices.update(dto);
            if (response == false)
            {
                return Unauthorized("Can't create");
            }
            return Ok(response);
        }
        [HttpDelete("Deleted/{id}")]
        public async Task<IActionResult> deleted([FromRoute]string id)
        {
            var response = await JewelryServices.deleteById(id);
            if (response == false)
            {
                return Unauthorized("Can't create");
            }
            return Ok(response);
        }
    }
}
