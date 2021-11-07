using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using my_nomination_api.models;
using my_nomination_api.Services;
using System;
using System.Collections.Generic;

namespace my_nomination_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public List<Category> GetAllCategories()
        {
            return _categoryService.GetAllCategories();
        }

        [HttpGet]
        [Route("GetCategoriesByRegion")]
        public List<Category> GetCategoriesByRegion(string regionId)
        {
            return _categoryService.GetCategoriesByRegion(regionId);
        }

        [HttpGet]
        [Route("GetCategoryById")]
        public Category GetCategoriesById(string categoryId)
        {
            return _categoryService.GetCategoriesById(categoryId);
        }

        [HttpPost]
        [Route("CreateCategory")]
        public ActionResult<Category> CreateCategory([FromBody] Category category)
        {
            var categoryExists = _categoryService.GetCategoriesByRegion(category.RegionId.ToString());
            if (categoryExists != null && categoryExists.Count > 0)
            {
                return BadRequest();
            }

            return _categoryService.CreateCategory(category);
        }

        [Route("UpdateCategory")]
        public ActionResult<Category> UpdateCategory([FromBody] Category category)
        {
            var categoryExists = _categoryService.GetCategoriesByRegion(category.RegionId.ToString());
            if (categoryExists == null && categoryExists.Count <= 0)
            {
                return BadRequest();
            }

            return _categoryService.UpdateCategory(category);
        }
    }
}
