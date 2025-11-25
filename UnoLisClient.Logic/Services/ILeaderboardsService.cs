using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UnoLisClient.Logic.Helpers;
using UnoLisClient.Logic.UnoLisServerReference.Leaderboards;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Services
{
    public interface ILeaderboardsService
    {
        Task<ServiceResponse<List<LeaderboardEntry>>> GetGlobalLeaderboardAsync();
    }
    public class LeaderboardsService : ILeaderboardsService
    {
        public async Task<ServiceResponse<List<LeaderboardEntry>>> GetGlobalLeaderboardAsync()
        {
            Console.WriteLine("[DEBUG] Llamando Leaderboards...");

            using (var client = new LeaderboardsManagerClient())
            {
                var result = await client.GetGlobalLeaderboardAsync();

                var list = result.Data != null
                    ? new List<LeaderboardEntry>(result.Data)
                    : new List<LeaderboardEntry>();

                return new ServiceResponse<List<LeaderboardEntry>>
                {
                    Code = result.Code,
                    Success = result.Success,
                    Data = list
                };
            }
        }
    }
}
