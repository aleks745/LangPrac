using LangPrac.Data;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace LangPrac.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(int chatId, string senderId, string content)
        {
            await _chatService.SendMessageAsync(chatId, senderId, content);
            await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", senderId, content);

            await _chatService.MarkMessagesAsReadAsync(chatId, senderId);
            await Clients.Group(chatId.ToString()).SendAsync("MessagesRead", senderId);
        }

        public async Task MarkMessageAsRead(int chatId, string userId)
        {
            await _chatService.MarkMessagesAsReadAsync(chatId, userId);
            await Clients.Group(chatId.ToString()).SendAsync("MessagesRead", userId);
        }

        public async Task JoinChat(int chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        public async Task LeaveChat(int chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
        }
    }
}