using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KursachV2.Models;
using Microsoft.AspNetCore.Identity;
using KursachV2.Services.PlayerService;
using KursachV2.Models.Players;

namespace KursachV2.Controllers
{
    public class MainController : Controller
    {
       
        private IPlayer _playerService;
        private readonly UserManager<User> _userManager;
        public MainController(  IPlayer playerService, UserManager<User> userManager)
        {
            _playerService = playerService;
            _userManager = userManager;
        }


        public async Task<IActionResult> MainPage()
        {
            if (User.Identity.IsAuthenticated)
            {
                User user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user != null)
                {
                    CommunityPlayer player = await _playerService.CPlayerInfo(user.Account_id);
                    return View(player);
                }
                else { return NotFound(); }
            }
            else
            {
               return RedirectToAction("Login" , "Account");
            }
        }
    }
}
