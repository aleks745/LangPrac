using LangPrac.Data;
using Microsoft.AspNetCore.SignalR;

namespace LangPrac.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task SendMessage(int chatId, string senderId, string content)
        {
            await _chatService.SendMessageAsync(chatId, senderId, content);
            await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", senderId, content);
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