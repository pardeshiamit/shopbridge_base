using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopbridge_base.Data;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;

namespace Shopbridge_base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService CategoryService;
        private readonly ILogger<CategoryController> logger;
        public CategoryController(ICategoryService _CategoryService)
        {
            this.CategoryService = _CategoryService;
        }

        [HttpGet]   
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            return  Ok(await CategoryService.GetAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var Category = await CategoryService.GetAsync(id);
            if(Category == null)
            {
                return NotFound();
            }
            return Ok(Category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category Category)
        {
            if(Category == null || id != Category.CategoryId)
            {
                return BadRequest();
            }
            await CategoryService.UpdateAsync(Category);
            return Ok(Category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category Category)
        {
            if (Category == null)
            {
                return BadRequest();
            }
            await CategoryService.CreateAsync(Category);
            return Ok(Category);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await CategoryService.DeleteAsync(id);
            return Ok();
        }

        private bool CategoryExists(int id)
        {
            return CategoryService.IsExists(id);
        }
    }
}
