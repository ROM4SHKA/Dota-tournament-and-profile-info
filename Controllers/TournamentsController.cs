using System;
using System.Collections.Generic;
using System.Linq;
using KursachV2.Models.Tournaments;
using System.Threading.Tasks;
using KursachV2.Services.TourService;
using Microsoft.AspNetCore.Mvc;

namespace KursachV2.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly ITournament _tournamentservice;
        public TournamentsController(ITournament tournamentservice)
        {
            _tournamentservice = tournamentservice;
        }


        public async Task<IActionResult> Tourinfo(string which)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (which == "past")
                {

                    List<Tournament> t = await _tournamentservice.GetPastTour();
                    return View(t);
                }
                if (which == "running")
                {
                    List<Tournament> t = await _tournamentservice.GetRunTour();
                    return View(t);
                }
                if (which == "upcomming")
                {
                    List<Tournament> t = await _tournamentservice.GetUpTour();
                    return View(t);
                }
                if (which == "all")
                {
                    List<Tournament> t = new List<Tournament>();
                    t.AddRange(await _tournamentservice.GetPastTour());
                    t.AddRange(await _tournamentservice.GetRunTour());
                    t.AddRange(await _tournamentservice.GetUpTour());
                    return View(t);
                }
                else
                {
                    return Content("Сторінка не знайдена");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}