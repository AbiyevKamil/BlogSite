using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Business.Abstract;

namespace Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IBlogService _blogService;

        public CategoriesController(ICategoryService categoryService, IBlogService blogService)
        {
            _categoryService = categoryService;
            _blogService = blogService;
        }
        
        public async Task<IActionResult> Index(string categoryName)
        {
            ViewBag.categoryName = categoryName;
            var blogs = await _blogService.GetByCategoryNameAsync(categoryName);
            return View(blogs);
        }
    }
}