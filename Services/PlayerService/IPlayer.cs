using System;
using System.Collections.Generic;
using System.Linq;
using KursachV2.Models.Players;
using System.Threading.Tasks;

namespace KursachV2.Services.PlayerService
{
    public interface IPlayer
    {
        Task<bool> IdIsValid(int? id);
        Task<CommunityPlayer> CPlayerInfo(int? id);
    }
}
