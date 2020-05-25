using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using KursachV2.ViewModel;
using KursachV2.Models;
using Microsoft.AspNetCore.Authorization;

namespace KursachV2.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;

        public RoleController(RoleManager<IdentityRole> roleMandger, UserManager<User> userManager)
        {
            _roleManager = roleMandger;
            _userManager = userManager;
        }
        public IActionResult RoleList()
        {
            return View(_roleManager.Roles.ToList());
        }
        [HttpGet]
        public IActionResult Create(CreateRoleModel model , string returnUrl=null) 
        { 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleModel model)
        {
            if (!string.IsNullOrEmpty(model.Name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("RoleList");
        }
        public IActionResult UserList() { return View(_userManager.Users.ToList()); }
        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
           
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleModel model = new ChangeRoleModel{UserId = user.Id, UserEmail = user.Email, UserRoles = userRoles,AllRoles = allRoles};
                return View(model);
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {         
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
               
                var addedRoles = roles.Except(userRoles);
               
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }
    }
}