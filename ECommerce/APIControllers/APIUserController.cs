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
        [HttpGet("GetProductByCat/{id}")]
        public IActionResult GetProductByCat(int id)
        {
            var products = _uservices.GetProductByCat(id);
            return Ok(products);
        }
        [HttpPost("AddToCart/{p_id}")]
        public IActionResult AddToCart(int p_id)
        {
            int userId = 1; // temporary (later session se lena)
            _uservices.AddToCart(userId, p_id);
            return Ok();
        }

        [HttpGet("GetCartItems")]
        public IActionResult GetCartItems()
        {
            int userId = 1;
            return Ok(_uservices.GetCartItems(userId));
        }

        [HttpPut("IncreaseQty/{p_id}")]
        public IActionResult IncreaseQty(int p_id)
        {
            int userId = 1;
            _uservices.IncreaseQty(userId, p_id);
            return Ok();
        }

        [HttpPut("DecreaseQty/{p_id}")]
        public IActionResult DecreaseQty(int p_id)
        {
            int userId = 1;
            _uservices.DecreaseQty(userId, p_id);
            return Ok();
        }

        [HttpGet("GetCartCount")]
        public IActionResult GetCartCount()
        {
            int userId = 1;
            return Ok(_uservices.GetCartCount(userId));
        }
    }
}
