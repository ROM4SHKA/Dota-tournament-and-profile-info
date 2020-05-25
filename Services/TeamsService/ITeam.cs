using System.Collections.Generic;
using KursachV2.Models.Team_Player;
using System.Threading.Tasks;

namespace KursachV2.Services.TeamsService
{ 
        public interface ITeam
        {
            Task<List<Team>> FullTeamList();
            Task<List<Team>> OneTeamInfo(int id);
       }  
}
