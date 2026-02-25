using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace ECommerce.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIAdminController : ControllerBase
    {
        private readonly IAdminRepository _aservices;
        private readonly IWebHostEnvironment _env;
        public APIAdminController(IAdminRepository aservices, IWebHostEnvironment env)
        {
            _aservices = aservices;
            _env = env;
        }
        [HttpPost("AddCategory")]
        public IActionResult AddCategory([FromForm] string C_Name, [FromForm] IFormFile C_Pic)
        {
            if (C_Pic == null)
                return BadRequest("Image missing");

            string fileName = DateTime.Now.Ticks + "_" + C_Pic.FileName;
            string path = Path.Combine(_env.WebRootPath, "images", fileName);
            Directory.CreateDirectory(Path.Combine(_env.WebRootPath, "images"));
            using (var stream = new FileStream(path, FileMode.Create))
                C_Pic.CopyTo(stream);
            CategoryModel catmod = new CategoryModel
            {
                C_Name = C_Name,
                C_Pic = fileName
            };
            _aservices.AddCategory(catmod);

            return Ok("Category Added");
        }
        [HttpGet("GetAllCategory")]
        public IActionResult GetAllCategory()
        {
            var result = _aservices.GetAllCategory();
            return Ok(result);
        }
        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromForm] ProductModel pmod, [FromForm] IFormFile P_Pic)
        {
            if (P_Pic == null)
                return BadRequest("Image is Missing");
            string fileName = DateTime.Now.Ticks + "_" + P_Pic.FileName;
            string folderPath = Path.Combine(_env.WebRootPath, "Images");

            // Create folder only if not exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // ✅ Correct full file path
            string fullPath = Path.Combine(folderPath, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                P_Pic.CopyTo(stream);
            }
            pmod.P_Pic = fileName;
            _aservices.AddProduct(pmod);
            return Ok("Product Added");
        }
        [HttpGet("GetAllProduct")]
        public IActionResult GetAllProduct()
        {
            var products = _aservices.GetAllProduct();
            return Ok(products);
        }
        [HttpGet("GetAllUser")]
        public IActionResult GetAllUser()
        {
            var result = _aservices.GetAllUser();
            return Ok(result);
        }
        [HttpGet("GetUserById/{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _aservices.GetUserById(id);
            return Ok(user);
        }
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser([FromBody] SignupModel user)
        {
            var existingUser = _aservices.GetUserById(user.Id);
            if (existingUser == null)
                return NotFound("User not found");
            _aservices.UpdateUser(user);
            return Ok("User Updated");
        }
        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var existingUser = _aservices.GetUserById(id);
            if (existingUser == null)
                return NotFound("User not found");
            _aservices.DeleteUser(id);
            return Ok("User Deleted");
        }
        [HttpGet("DashboardCounts")]
        public IActionResult DashboardCounts()
        {
            var data = new
            {
                users = _aservices.GetAllUser().Count(),
                categories = _aservices.GetAllCategory().Count(),
                products = _aservices.GetAllProduct().Count()
            };
            return Ok(data);
        }
        [HttpGet("GetCatById/{id}")]
        public IActionResult GetCatById(int id)
        {
            var result = _aservices.GetCatById(id);
            return Ok(result);
        }
        [HttpPut("UpdateCategory")]
        public IActionResult UpdateCategory([FromForm] CategoryModel catmod, [FromForm] IFormFile C_Pic, [FromForm] string old_pic)
        {
            var existingCat = _aservices.GetCatById(catmod.C_ID);

            if (existingCat == null)
                return NotFound();

            existingCat.C_Name = catmod.C_Name;

            if (C_Pic != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(C_Pic.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    C_Pic.CopyTo(stream);
                }

                existingCat.C_Pic = fileName;
            }
            else
            {
                existingCat.C_Pic = old_pic;
            }

            _aservices.UpdateCategory(existingCat);

            return Ok();
        }
        [HttpDelete("DeleteCategory/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var existingCat = _aservices.GetCatById(id);
            if (existingCat == null)
                return NotFound("Category not found");
            _aservices.DeleteCategory(id);
            return Ok("Category Deleted");
        }
        [HttpGet("GetProductById/{id}")]
        public IActionResult GetProductById(int id)
        {
            var result = _aservices.GetProductById(id);
            return Ok(result);
        }
        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct([FromForm] ProductModel product, [FromForm] IFormFile P_Pic, [FromForm] string old_pic)
        {
            var existingProduct = _aservices.GetProductById(product.P_Id);
            if (existingProduct == null)
                return NotFound("Product not found");
            existingProduct.P_Name = product.P_Name;
            existingProduct.P_Desc = product.P_Desc;
            existingProduct.P_Price = product.P_Price;
            existingProduct.P_Cat = product.P_Cat;
            if (P_Pic != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(P_Pic.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    P_Pic.CopyTo(stream);
                }
                existingProduct.P_Pic = fileName;
            }
            else
            {
                existingProduct.P_Pic = old_pic;
            }
            _aservices.UpdateProduct(existingProduct);
            return Ok("Product Updated");
        }
        [HttpDelete("DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var existingProduct = _aservices.GetProductById(id);
            if (existingProduct == null)
                return NotFound("Product not found");
            _aservices.DeleteProduct(id);
            return Ok("Success");
        }
    }
}
