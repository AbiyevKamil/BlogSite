using System.Threading.Tasks;
using Core;
using Core.Constants;
using Core.Entities;
using Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Business.Abstract;
using Web.Models.Auth;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;
        private readonly EmailService _emailService;

        public AuthController(SignInManager<User> signInManager, IUserService userService,
            EmailService emailService)
        {
            _signInManager = signInManager;
            _userService = userService;
            _emailService = emailService;
        }

        [HttpGet, AllowOnlyAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet, AllowOnlyAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, AllowOnlyAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var result = await _signInManager
                        .PasswordSignInAsync(user, model.Password, model.KeepMeSignedIn, false);

                    if (result.Succeeded)
                    {
                        var isAdmin = await _userService.IsInRoleAsync(user, Roles.ADMIN);
                        if (isAdmin)
                            return RedirectToAction("Index", "Admin");
                        var isUser = await _userService.IsInRoleAsync(user, Roles.USER);
                        if (isUser)
                            return RedirectToAction("Index", "Home");
                    }

                    if (result.IsNotAllowed)
                        ModelState.AddModelError("", "Please confirm email, then try again.");
                }

                ModelState.AddModelError("", "Email is not registered");
            }

            return View(model);
        }

        [HttpPost, AllowOnlyAnonymous]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    FullName = model.FullName,
                    Description = model.Description,
                };

                var result = await _userService.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    var resultRole = await _userService.AddToRoleAsync(newUser, Roles.USER);
                    if (resultRole.Succeeded)
                    {
                        return RedirectToAction("SendConfirmEmail", "Auth", new {email = model.Email});
                    }

                    ModelState.AddModelError("", "Oops, something went wrong. Try again later. :(");
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet, AllowOnlyAnonymous]
        public async Task<IActionResult> SendConfirmEmail([FromQuery] string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var user = await _userService.FindByEmailAsync(email);
                if (user is not null)
                {
                    if (user.EmailConfirmed)
                    {
                        ViewBag.Status = "Email confirmed confirmed already.";
                        return View();
                    }
                }

                var isSend = await SendConfirmUrlAsync(email);
                if (isSend)
                {
                    ViewBag.Status = "Confirmation link sent. Check your email.";
                    return View(model: email);
                }

                ViewBag.Status = "Oops, something went wrong. Try again later. :(";
                return View(model: email);
            }

            ViewBag.Status = "Email is required.";
            return View(model: email);
        }

        [HttpGet, AllowOnlyAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userEmail, string token)
        {
            var user = await _userService.FindByEmailAsync(userEmail);
            if (user is not null)
            {
                if (user.EmailConfirmed)
                {
                    ViewBag.Status = "Email is already confirmed";
                    return View();
                }
                var result = await _userService.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    ViewBag.Status = "Email confirmed successfully. Now you can login.";
                    return View();
                }

                ViewBag.Status = "Something went wrong. Try again later.";
                ViewBag.Error = true;
                return View();
            }

            ViewBag.Status = "Wrong url, try again.";
            ViewBag.Error = true;
            return View();
        }


        private async Task<bool> SendConfirmUrlAsync(string email)
        {
            var user = await _userService.FindByEmailAsync(email);
            var token = await _userService.GetEmailConfirmationToken(user);
            var url = Url.Action(nameof(ConfirmEmail), "Auth", new {userEmail = user.Email, token},
                Request.Scheme);
            return _emailService.SendEmail(url, user.Email);
        }
    }
}