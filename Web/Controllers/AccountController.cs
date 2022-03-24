using System.Threading.Tasks;
using Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Business.Abstract;
using Web.Helpers;
using Web.Models.Account;

namespace Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IBlogService _blogService;
        private readonly ICommentService _commentService;
        private readonly FileService _fileService;

        public AccountController(IUserService userService, IBlogService blogService, ICommentService commentService,
            FileService fileService)
        {
            _userService = userService;
            _blogService = blogService;
            _commentService = commentService;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userService.FindByClaimsAsync(User);
            var blogs = await _blogService.GetAllByUserAsync(user);
            var comments = await _commentService.GetAllByUserAsync(user);
            var model = new IndexModel
            {
                User = user,
                Blogs = blogs,
                Comments = comments
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProfilePhoto(IFormFile picture)
        {
            if (picture is not null)
            {
               var filePath = await _fileService.UploadImage(picture, UploadFileType.ProfilePicture);
               if (!string.IsNullOrEmpty(filePath))
               {
                   var user = await _userService.FindByClaimsAsync(User);
                   user.ProfilePicture = filePath;
                   var result = await _userService.UpdateUserAsync(user);
                   if (result.Succeeded)
                       return RedirectToAction("Index");
                   ViewBag.Error = true;
                   ViewBag.Status = "Oops, something went wrong";
                   return RedirectToAction("Index");
               }
            }

            ViewBag.Error = true;
            ViewBag.Status = "Choose picture";
            return RedirectToAction("Index");
        }
    }
}