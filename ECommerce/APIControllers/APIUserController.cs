using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIUserController : ControllerBase
    {
        private readonly IUserRepository _uservices;
        public APIUserController(IUserRepository uservices)
        {
            _uservices = uservices;
        }
        [HttpGet("GetAllCategory")]
        public IActionResult GetAllCategory()
        {
            var Category = _uservices.GetAllCategory();
            return Ok(Category);
        }
        [HttpGet("GetAllProduct")]
        public IActionResult GetAllProduct()
        {
            var Products = _uservices.GetAllProducts();
            return Ok(Products);    
        }
    }
}
