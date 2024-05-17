namespace LangPrac.Data
{
    public interface IChatService
    {
        Task<Chat> GetChatAsync(int chatId);
        Task<List<Chat>> GetUserChatsAsync(string userId);
        Task<Chat> CreateChatAsync(string user1Id, string user2Id);
        Task SendMessageAsync(int chatId, string senderId, string content);
        Task CreateEndPracticeNotification(string senderId, string receiverId);
        Task DeleteChatAsync(int chatId);
    }
}