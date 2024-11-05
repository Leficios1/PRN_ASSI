using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace PRN_ASSI_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _services;

        public CategoryController(ICategoryServices services)
        {
            _services = services;
        }
        [HttpGet("getAll")]
        public IActionResult getAll()
        {
            var result = _services.GetCategories();
            return Ok(result);
        }
        [HttpGet("getById/{id}")]
        public IActionResult getById([FromRoute]string id)
        {
            var result = _services.GetCategories(id);
            return Ok(result);
        }
    }
}
