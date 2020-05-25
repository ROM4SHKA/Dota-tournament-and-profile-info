using System;
using System.Collections.Generic;
using KursachV2.Models.Tournaments;
using System.Threading.Tasks;

namespace KursachV2.Services.TourService
{
   
        public interface ITournament
        {
            Task<List<Tournament>> GetPastTour();
            Task<List<Tournament>> GetRunTour();
            Task<List<Tournament>> GetUpTour();
        }
    
}

