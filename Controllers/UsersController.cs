using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using KursachV2.Models;
using KursachV2.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace KursachV2.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        UserManager<User> _userManager;
        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult UserList() { return View(_userManager.Users.ToList()); }
       [HttpGet]
        public IActionResult Create(CreateModel model, string returnUrl = null)
        {
            if (model.Danger >= 1)
            {
                if (model.Danger == 2)
                {
                    ModelState.AddModelError("", "Коритувач з таким email уже існує.");
                }
                if (model.Danger == 3)
                {
                    ModelState.AddModelError("", "Некоректний SteamID32, перевірте чи це дійсно ваш акккаунт, корекність введених даних.");
                }
                return View(model);
            }
            return View(new CreateModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                var user1 = await _userManager.FindByEmailAsync(model.Email);
                if (user1 != null)
                {
                    model.Danger = 2;
                    model.Gone++;
                    return RedirectToAction("Create", "Users", model);
                }
                User user = new User { Email = model.Email, UserName = model.Email, Account_id = model.Account_id };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserList");
                }
                else
                {
                    model.Gone++;
                    model.Danger = 3;
                }
            }
            else
            {
                model.Gone++;
                model.Danger = 1;
            }
            return RedirectToAction("Create", "Users", model);
        }
        public async Task<IActionResult> Edit(EditModel model, string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null) { return NotFound(); }

            if (model.Danger >= 1)
            {
                if (model.Danger == 3)
                {
                    ModelState.AddModelError("", "Некоректний SteamID32, перевірте чи це дійсно ваш акккаунт, корекність введених даних.");
                }
                if (model.Danger == 2)
                {
                    ModelState.AddModelError("", "Користувач з таким email вже існує.");
                }
                model.OldEmail = user.Email;
                model.OldAccount_id = user.Account_id;
                return View(model);
            }     
            EditModel editModel = new EditModel { Id = user.Id, Email = user.Email, Account_id = user.Account_id, OldAccount_id = user.Account_id, OldEmail = user.Email };     
            return View(editModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditModel model)
        {
            if (ModelState.IsValid)
            {
                if(await _userManager.FindByEmailAsync(model.Email) != null)
                {
                    model.Gone++;
                    model.Danger = 2;
                    return RedirectToAction("Edit", "Users", model);
                }
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.Account_id = model.Account_id;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("UserList");
                    }
                    else
                    {
                        model.Gone++;
                        model.Danger = 3;
                        return RedirectToAction("Edit", "Users", model);
                    }
                }
            }
            model.Gone++;
            model.Danger = 1;
            return RedirectToAction("Edit", "Users", model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete (string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if(user!= null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("UserList");
        }
        public async Task<IActionResult> ChangePassword( ChangePasswordModel model,string id)
        {
            if (model.Danger >= 1)
            {
                if (model.Danger == 2)
                {
                    ModelState.AddModelError("", "Неправильно введений старий пароль.");
                }
                return View(model);
            }
            User user = await _userManager.FindByIdAsync(id);
            if (user == null) { return NotFound(); }
            ChangePasswordModel model1 = new ChangePasswordModel { Id = user.Id, Email = user.Email };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if(user!= null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("UserList");
                    }
                    else
                    {
                        model.Gone++;
                        model.Danger = 2;
                    }
                }
                else { ModelState.AddModelError(string.Empty, "Користувач не знайдений."); }
              
            }
            model.Gone++;
            model.Danger = 1;
            return RedirectToAction("ChangePassword", "Users", model);
        }
    }
}