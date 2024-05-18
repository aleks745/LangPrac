namespace LangPrac.Data
{
    public interface IChatService
    {
        Task<Chat> GetChatAsync(int chatId);
        Task<List<Chat>> GetUserChatsAsync(string userId);
        Task<Chat> CreateChatAsync(string user1Id, string user2Id);
        Task CreateChatConfirmationNotification(string senderId, string receiverId);
        Task SendInviteWithDetails(ApplicationUser sender, ApplicationUser receiver);
        Task SendMessageAsync(int chatId, string senderId, string content);
        Task CreateEndPracticeNotification(string senderId, string receiverId);
        Task RatePartnerAsync(string partnerId, int rating, int notificationId);
        Task DeleteChatAsync(int chatId);
    }
}