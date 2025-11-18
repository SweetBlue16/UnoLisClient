using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;
using UnoLisClient.Logic.UnoLisServerReference.Matchmaking;
using UnoLisClient.Logic.Helpers;

using MatchSettings = UnoLisClient.Logic.UnoLisServerReference.Matchmaking.MatchSettings;
using CreateMatchResponse = UnoLisClient.Logic.UnoLisServerReference.Matchmaking.CreateMatchResponse;
using JoinMatchResponse = UnoLisClient.Logic.UnoLisServerReference.Matchmaking.JoinMatchResponse;

namespace UnoLisClient.Logic.Services
{
    /// <summary>
    /// Singleton implementation of IMatchmakingService.
    /// Wraps the WCF proxy for creating matches.
    /// </summary>
    public class MatchmakingService : IMatchmakingService
    {
        private static readonly Lazy<MatchmakingService> _instance =
            new Lazy<MatchmakingService>(() => new MatchmakingService());

        public static IMatchmakingService Instance => _instance.Value;

        private MatchmakingService()
        {
        }

        public async Task<CreateMatchResponse> CreateMatchAsync(MatchSettings settings)
        {
            MatchmakingManagerClient proxy = null;
            try
            {
                proxy = new MatchmakingManagerClient("NetTcpBinding_IMatchmakingManager");

                var response = await proxy.CreateMatchAsync(settings);

                proxy.Close();
                return response;
            }
            catch (TimeoutException ex)
            {
                AbortProxy(proxy);
                LogManager.Error($"Timeout calling CreateMatchAsync: {ex.Message}", ex);
                return new CreateMatchResponse 
                { 
                    Success = false, 
                    Message = "Server timeout." 
                };
            }
            catch (CommunicationException ex)
            {
                AbortProxy(proxy);
                LogManager.Error($"Communication error in CreateMatchAsync: {ex.Message}", ex);
                return new CreateMatchResponse 
                { 
                    Success = false, 
                    Message = "Network error." 
                };
            }
            catch (Exception ex)
            {
                AbortProxy(proxy);
                LogManager.Error($"Fatal error in CreateMatchAsync: {ex.Message}", ex);
                return new CreateMatchResponse 
                { 
                    Success = false, 
                    Message = "An unexpected error occurred." 
                };
            }
        }

        public async Task<JoinMatchResponse> JoinMatchAsync(string lobbyCode, string nickname)
        {
            MatchmakingManagerClient proxy = null;
            try
            {
                proxy = new MatchmakingManagerClient("NetTcpBinding_IMatchmakingManager");

                var response = await proxy.JoinMatchAsync(lobbyCode, nickname);

                proxy.Close();
                return response;
            }
            catch (TimeoutException ex)
            {
                AbortProxy(proxy);
                LogManager.Error($"Timeout joining match {lobbyCode}: {ex.Message}", ex);
                return new JoinMatchResponse { Success = false, Message = "Server timeout joining match." };
            }
            catch (CommunicationException ex)
            {
                AbortProxy(proxy);
                LogManager.Error($"Network error joining match {lobbyCode}: {ex.Message}", ex);
                return new JoinMatchResponse { Success = false, Message = "Connection error. Check your internet." };
            }
            catch (Exception ex)
            {
                AbortProxy(proxy);
                LogManager.Error($"Unexpected error joining match {lobbyCode}: {ex.Message}", ex);
                return new JoinMatchResponse { Success = false, Message = "An unexpected error occurred." };
            }
        }

        private void AbortProxy(ICommunicationObject proxy)
        {
            if (proxy != null)
            {
                proxy.Abort();
            }
        }
    }
}
