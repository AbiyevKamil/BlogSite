using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.Constants;
using Core.Entities;
using Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Business.Abstract;
using Web.Helpers;
using Web.Models.Blogs;

namespace Web.Controllers
{
    [Authorize]
    public class BlogsController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;
        private readonly FileService _fileService;

        public BlogsController(IBlogService blogService, IUserService userService,
            ICategoryService categoryService, ICommentService commentService, FileService fileService)
        {
            _blogService = blogService;
            _userService = userService;
            _categoryService = categoryService;
            _commentService = commentService;
            _fileService = fileService;
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> Details(string uniqueUrl)
        {
            if (!string.IsNullOrEmpty(uniqueUrl))
            {
                var blog = await _blogService.FindPublicByUrlAsync(uniqueUrl);
                if (blog is null)
                {
                    return RedirectToAction("Index", "Home");
                }

                await _blogService.ViewedBlogAsync(blog);
                ViewBag.PopularBlogs = await _blogService.GetPopularBlogsAsync(3);
                ViewBag.MoreBlogs = await _blogService.GetPopularBlogsAsync(8);
                return View(blog);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> NewBlog()
        {
            var categories = await _categoryService.GetAllAsync();
            var categorySelects = categories.Select(i =>
                new SelectListItem(i.Name, i.Id.ToString())).ToList();
            @ViewBag.Categories = categorySelects;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewBlog(NewBlogModel model)
        {
            if (ModelState.IsValid)
            {
                var filePath = await _fileService.UploadImage(model.Banner, UploadFileType.Banner);
                if (!string.IsNullOrEmpty(filePath))
                {
                    var user = await _userService.FindByClaimsAsync(User);
                    var blog = new Blog
                    {
                        Banner = filePath,
                        Content = model.Content,
                        Title = model.Title,
                        CategoryId = model.CategoryId,
                        UniqueUrl = UrlGenerator.Generate(model.Title),
                        User = user,
                    };
                    await _blogService.CreateAsync(blog);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Something went wrong try again later.");
            }
            var categories = await _categoryService.GetAllAsync();
            var categorySelects = categories.Select(i =>
                new SelectListItem(i.Name, i.Id.ToString())).ToList();
            @ViewBag.Categories = categorySelects;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int blogId, string message)
        {
            var blog = await _blogService.FindPublicByIdAsync(blogId);
            if (blog is not null)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    var user = await _userService.FindByClaimsAsync(User);
                    var comment = new Comment
                    {
                        User = user,
                        Blog = blog,
                        Content = message
                    };
                    await _blogService.CreateCommentAsync(blog, comment);
                }

                return RedirectToAction("Details", "Blogs", new {uniqueUrl = blog.UniqueUrl});
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> DeleteBlog(int blogId)
        {
            var blog = await _blogService.FindPublicByIdAsync(blogId);
            if (blog is not null)
            {
                var user = await _userService.FindByClaimsAsync(User);
                if (user.Id == blog.UserId)
                {
                    await _blogService.SoftDeleteAsync(blog);
                    return RedirectToAction("Index", "Home");
                }

                return RedirectToAction("Details", "Blogs", new {uniqueUrl = blog.UniqueUrl});
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(int blogId, int commentId)
        {
            var blog = await _blogService.FindPublicByIdAsync(blogId);
            if (blog is not null)
            {
                var user = await _userService.FindByClaimsAsync(User);
                var comment = await _commentService.GetByIdAsync(commentId);
                if (comment is not null)
                {
                    if (user.Id == comment.UserId)
                    {
                        await _commentService.DeleteAsync(comment);
                    }
                }

                return RedirectToAction("Details", "Blogs", new {uniqueUrl = blog.UniqueUrl});
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBlog([FromQuery] string uniqueUrl)
        {
            if (!string.IsNullOrEmpty(uniqueUrl))
            {
                var blog = await _blogService.FindPublicByUrlAsync(uniqueUrl);
                if (blog is not null)
                {
                    var user = await _userService.FindByClaimsAsync(User);
                    if (blog.UserId == user.Id)
                    {
                        var categories = await _categoryService.GetAllAsync();
                        var categorySelects = categories.Select(i =>
                            new SelectListItem(i.Name, i.Id.ToString())).ToList();
                        @ViewBag.Categories = categorySelects;
                        var model = new UpdateBlogModel
                        {
                            Id = blog.Id,
                            Title = blog.Title,
                            Content = blog.Content,
                            CategoryId = blog.CategoryId
                        };
                        return View(model);
                    }

                    return RedirectToAction("Details", "Blogs", new {uniqueUrl = blog.UniqueUrl});
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBlog(UpdateBlogModel model)
        {
            if (!string.IsNullOrEmpty(model.UniqueUrl))
            {
                var blog = await _blogService.FindPublicByUrlAsync(model.UniqueUrl);
                if (blog is not null)
                {
                    var user = await _userService.FindByClaimsAsync(User);
                    if (blog.UserId == user.Id)
                    {
                        var categories = await _categoryService.GetAllAsync();
                        var categorySelects = categories.Select(i =>
                            new SelectListItem(i.Name, i.Id.ToString())).ToList();
                        @ViewBag.Categories = categorySelects;
                        var updatedBlog = new Blog
                        {
                            Title = model.Title,
                            Content = model.Content,
                            Banner = model.Banner is null ? blog.Banner : await _fileService.UploadImage(model.Banner,UploadFileType.Banner),
                            Category = model.Category,
                            CategoryId = model.CategoryId
                        };
                        await _blogService.UpdateAsync(blog.Id, updatedBlog);
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}