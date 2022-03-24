using System.Threading.Tasks;
using Core.Constants;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Business.Abstract;
using Web.Models.Admin;

namespace Web.Controllers
{
    [Authorize(Roles = Roles.ADMIN)]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;

        public AdminController(IUserService userService, IBlogService blogService, ICategoryService categoryService,
            ICommentService commentService)
        {
            _userService = userService;
            _blogService = blogService;
            _categoryService = categoryService;
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var blogs = await _blogService.GetAllAsync();
            var comments = await _commentService.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();
            var model = new AdminModel
            {
                Blogs = blogs,
                Categories = categories,
                Comments = comments
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int? categoryId)
        {
            if (categoryId is not null)
            {
                ViewBag.CategoryId = categoryId;
            }

            var blogs = await _blogService.GetAllAsync();
            var comments = await _commentService.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();
            var model = new AdminModel
            {
                Blogs = blogs,
                Categories = categories,
                Comments = comments
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var comment = await _commentService.GetByIdAsync(commentId);
            if (comment is not null)
            {
                await _commentService.DeleteAsync(comment);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(string categoryName)
        {
            var exist = await _categoryService.FindByNameAsync(categoryName);
            if (exist is null)
            {
                await _categoryService.CreateAsync(new Category
                {
                    Name = categoryName
                });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var exist = await _categoryService.GetByIdAsync(categoryId);
            if (exist is not null)
            {
                await _categoryService.DeleteAsync(exist);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(int categoryId, string categoryName)
        {
            var exist = await _categoryService.GetByIdAsync(categoryId);
            if (exist is not null)
            {
                exist.Name = categoryName;
                await _categoryService.UpdateAsync(categoryId, exist);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ApproveBlog(int blogId)
        {
            await _blogService.ApproveAsync(blogId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBlog(int blogId)
        {
            var exist = await _blogService.GetByIdAsync(blogId);
            if (exist is not null)
            {
                await _blogService.SoftDeleteAsync(exist);
            }

            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> RestoreBlog(int blogId)
        {
            var exist = await _blogService.GetByIdAsync(blogId);
            if (exist is not null)
            {
                await _blogService.RestoreAsync(exist);
            }

            return RedirectToAction("Index");
        }
    }
}