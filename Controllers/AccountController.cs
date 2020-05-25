using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KursachV2.ViewModel;
using Microsoft.AspNetCore.Identity;
using KursachV2.Models;
using Microsoft.AspNetCore.Authorization;
using KursachV2.Services;

namespace KursachV2.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signinManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager )
        {
            _signinManager = signInManager;
            _userManager = userManager;          
        }

        public ResetPasswordModel ResetPasswordModel
        {
            get => default;
            set
            {
            }
        }

        [HttpGet]
        public IActionResult Register(RegisterModel model,string returnUrl = null)
        {
            if (model.Danger >= 1)
            {
                if (model.Danger == 3)
                {
                    ModelState.AddModelError("", "Коритувач з таким email уже існує.");
                }
                if (model.Danger == 2)
                {
                    ModelState.AddModelError("", "Некоректний SteamID32, перевірте чи це дійсно ваш акккаунт, корекність введених даних.");
                }
                return View(model);
            }
            return View(new RegisterModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User() { Email = model.Email, UserName = model.Email, Account_id = model.Account_id };

                var user1 = await _userManager.FindByEmailAsync(user.Email);
                if (user1 != null)
                {
                    model.Danger = 3;
                    model.Gone++;
                    return RedirectToAction("Register", "Account", model);
                }
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var code1 = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code1 }, protocol: HttpContext.Request.Scheme);
                    var emailService = new EmailService();
                    await emailService.SendEmailAsync(model.Email, "Confirm your account",
                        $"Підтвердіть реєстрацію, перейдіть за цим  <a href ='{callbackUrl}'>посиланням на сторінку входу</a>.");
                    await _userManager.AddToRoleAsync(user, "user");

                    return RedirectToAction("Succsess", "Account");
                }
                else
                {
                    model.Danger = 2;
                    model.Gone++;
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                }
            }
            else
            {
                model.Gone++;
                model.Danger = 1;
            }
            return RedirectToAction("Register", "Account", model);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            return View("Error");

        }
      
        [HttpGet]
        public async Task<IActionResult> Login(LoginModel model,string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signinManager.SignOutAsync();
            }
            if (model.Danger >= 1)
            {
                if (model.Danger == 2)
                {
                    ModelState.AddModelError("", "Неправильный логін і (або) пароль.");
                }
                return View(model);
            }
            return View(new LoginModel { ReturnUrl = returnUrl});
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user1 = await _userManager.FindByEmailAsync(model.Email);
                if (user1!= null)
                {
                    if (await _userManager.IsEmailConfirmedAsync(user1) || await _userManager.IsInRoleAsync(user1, "admin"))
                    {
                        var result =
                            await _signinManager.PasswordSignInAsync(user1.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                            {
                                return Redirect(model.ReturnUrl);
                            }
                            else
                            {
                                return RedirectToAction("MainPage", "Main");
                            }
                        }
                        else
                        {                         
                            model.Gone++;
                            model.Danger = 2;
                            return RedirectToAction("Login", "Account", model);
                        }
                    }
                    else
                    {                       
                        model.Gone++;
                        model.Danger = 2;
                        return RedirectToAction("Login", "Account", model);
                    }
                }
                else
                {                    
                    model.Gone++;
                    model.Danger = 2;
                    return RedirectToAction("Login", "Account", model);
                }
            }
            else
            {
                model.Gone++;
                model.Danger = 1;
            }
            return RedirectToAction("Login", "Account", model);
        }
        [HttpGet]
        public  IActionResult Error()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();
            return Redirect("~/Account/Login");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword(ForgotPasswordModel model, string returnUrl=null)
        {
            if (model.Danger >= 1)
            {
                if (model.Danger == 2)
                {
                    ModelState.AddModelError("", "Користувач з таким email не знайдений.");
                }
                return View(model);
            }
            return View(new ForgotPasswordModel());
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    model.Gone++;
                    model.Danger = 2;
                    return RedirectToAction("ForgotPassword", "Account", model);
                }
                var code1 = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code1, email = user.Email}, protocol: HttpContext.Request.Scheme);
                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(model.Email, "Reset Password",
                    $"Для зміни паролю  перейдіть за цим  <a href ='{callbackUrl}'>посиланням</a>.");
                return View("ForgotPasswordOK","Account");
            }
            model.Danger = 1;
            model.Gone++;
            return RedirectToAction("ForgotPassword", "Account", model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(ResetPasswordModel model,string email,string code = null)
        {
            if (model.Danger >= 1)
            {
                if (model.Danger == 2)
                {
                    ModelState.AddModelError("", "Користувач з таким email не знайдений.");
                }
                return View(model);
            }
            return code == null ? View("Error") : View( new ResetPasswordModel { Email =email});
        } 
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Gone++;
                model.Danger = 2;
                return RedirectToAction("ResetPassword", "Account", model);
            }
            User user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return View("ResetPasswordConfirmation","Account");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            model.Gone++;
            model.Danger = 2;
            return RedirectToAction("ResetPassword", "Account", model);
        }
    }
}
   
