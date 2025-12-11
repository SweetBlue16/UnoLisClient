using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using UnoLisClient.Logic.Helpers;
using System.Threading.Tasks;
using UnoLisClient.Logic.Callbacks;
using UnoLisClient.Logic.UnoLisServerReference.Chat;

namespace UnoLisClient.Logic.Services
{
    public class ChatService : IChatService
    {
        private static readonly Lazy<ChatService> _instance =
            new Lazy<ChatService>(() => new ChatService());

        public static IChatService Instance => _instance.Value;

        private IChatManager _proxy;
        private ChatCallback _chatCallback;
        private DuplexChannelFactory<IChatManager> _factory;
        private string _currentUserNickname;

        public event Action<ChatMessageData> OnMessageReceived;
        public event Action<ChatMessageData[]> OnChatHistoryReceived;
        public event Action<string> OnPlayerDisconnected;
        public event Action OnSessionExpired;

        private ChatService()
        {
            ChatCallback.OnMessageReceived += (msg) => OnMessageReceived?.Invoke(msg);
            ChatCallback.OnChatHistoryReceived += (msgs) => OnChatHistoryReceived?.Invoke(msgs);
            ChatCallback.OnPlayerDisconnected += (name) => OnPlayerDisconnected?.Invoke(name);
            ChatCallback.OnSessionExpired += () => OnSessionExpired?.Invoke();
        }

        public void Initialize(string nickname)
        {
            if (_factory != null && _factory.State != CommunicationState.Opened)
            {
                Cleanup();
            }

            if (_factory != null)
            {
                return;
            }

            try
            {
                _currentUserNickname = nickname;
                _chatCallback = new ChatCallback();
                var context = new InstanceContext(_chatCallback);

                _factory = new DuplexChannelFactory<IChatManager>(context, "NetTcpBinding_IChatManager");

                _proxy = _factory.CreateChannel();

                if (_proxy is ICommunicationObject channel)
                {
                    ServerConnectionMonitor.Monitor(channel);
                }

                _proxy.RegisterPlayer(_currentUserNickname);
            }
            catch (TimeoutException ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ChatService.Initialize] Timeout: {ex.Message}");
                Cleanup();
                throw;
            }
            catch (EndpointNotFoundException ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ChatService.Initialize] Endpoint not found. Is the server running? {ex.Message}");
                Cleanup();
                throw;
            }
            catch (CommunicationException ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ChatService.Initialize] Communication error: {ex.Message}");
                Cleanup();
                throw;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ChatService.Initialize] Unexpected error: {ex.Message}");
                Cleanup();
                throw;
            }
        }

        public void Cleanup()
        {
            if (_factory == null)
            {
                return;
            }

            try
            {
                if (_factory.State == CommunicationState.Opened)
                {
                    _factory.Close();
                }
                else
                {
                    _factory.Abort();
                }
            }
            catch (Exception)
            {
                _factory.Abort();
            }
            finally
            {
                _proxy = null;
                _factory = null;
                _currentUserNickname = null;
            }
        }

        public Task JoinChannelAsync(string channelId)
        {
            try
            {
                _proxy.GetChatHistory(channelId);
                return Task.CompletedTask;
            }
            catch(NullReferenceException ex)
            {
                Logger.Error($"[ChatService.JoinChannel] Proxy is null: {ex.Message}");
                throw;
            }
            catch (TimeoutException ex)
            {
                Logger.Error($"[ChatService.JoinChannel] Timeout: {ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                Logger.Error($"[ChatService.JoinChannel] Channel faulted: {ex.Message}");
                Cleanup();
                throw new CommunicationException("Chat channel was faulted, please reconnect.", ex);
            }
            catch (CommunicationException ex)
            {
                Logger.Error($"[ChatService.JoinChannel] Communication error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error($"[ChatService.JoinChannel] Unexpected error: {ex.Message}");
                throw;
            }
        }

        public Task LeaveChannelAsync(string channelId)
        {
            return Task.CompletedTask;
        }

        public Task SendMessageAsync(ChatMessageData message)
        {
            if (string.IsNullOrWhiteSpace(message.Message) || _proxy == null)
            {
                return Task.CompletedTask;
            }

            try
            {
                _proxy.SendMessage(message);
                return Task.CompletedTask;
            }
            catch (TimeoutException ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ChatService.SendMessage] Timeout: {ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ChatService.SendMessage] Channel faulted: {ex.Message}");
                Cleanup();
                throw new CommunicationException("Chat channel was faulted, please reconnect.", ex);
            }
            catch (CommunicationException ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ChatService.SendMessage] Communication error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ChatService.SendMessage] Unexpected error: {ex.Message}");
                throw;
            }
        }
    }
}
