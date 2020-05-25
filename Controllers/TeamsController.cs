using System;
using System.Collections.Generic;
using KursachV2.Models.Team_Player;
using System.Linq;
using KursachV2.Services.TeamsService;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace KursachV2.Controllers
{
    
    public class TeamsController : Controller
    {
       private  readonly ITeam _teamservice;
        public TeamsController(ITeam teamservice)
        {
            _teamservice = teamservice;
        }

        public async Task<IActionResult> TeamsPage()
        {
            if (User.Identity.IsAuthenticated)
            {
                IEnumerable<Team> list = await _teamservice.FullTeamList();
                var sortedlist = from l in list
                                 orderby l.Name
                                 select l;

                return View(sortedlist);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }      
        public async Task<IActionResult> TeamInfo(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                List<Team> t = await _teamservice.OneTeamInfo(id);
                Team x = t[0];
                return View(x);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}