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
            var client = new LeaderboardsManagerClient();

            try
            {
                var result = await client.GetGlobalLeaderboardAsync();

                var list = result.Data != null
                    ? new List<LeaderboardEntry>(result.Data)
                    : new List<LeaderboardEntry>();

                CloseClientHelper.CloseClient(client);

                return new ServiceResponse<List<LeaderboardEntry>>
                {
                    Code = result.Code,
                    Success = result.Success,
                    Data = list
                };
            }
            catch (TimeoutException ex)
            {
                Logger.Warn($"[LEADERBOARD] Timeout occurred while fetching global leaderboard.'{ex.Message}'");
                CloseClientHelper.CloseClient(client);
                return new ServiceResponse<List<LeaderboardEntry>>
                {
                    Success = false,
                    Code = UnoLisServer.Common.Enums.MessageCode.Timeout,
                    Data = new List<LeaderboardEntry>()
                };
            }
            catch (Exception ex)
            {
                Logger.Error($"[LEADERBOARD] Error fetching data: {ex.Message}", ex);
                CloseClientHelper.CloseClient(client);

                return new ServiceResponse<List<LeaderboardEntry>>
                {
                    Success = false,
                    Code = UnoLisServer.Common.Enums.MessageCode.ConnectionFailed,
                    Data = new List<LeaderboardEntry>()
                };
            }
        }
    }
}
