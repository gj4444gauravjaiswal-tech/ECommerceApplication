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
    }
}
