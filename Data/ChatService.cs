using LangPrac.Data;
using Microsoft.EntityFrameworkCore;

namespace LangPrac.Services
{
    public class ChatService : IChatService
    {
        private readonly ApplicationDbContext _context;

        public ChatService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Chat> GetChatAsync(int chatId)
        {
            return await _context.Chats
                .Include(c => c.Messages)
                    .ThenInclude(m => m.Sender)
                .FirstOrDefaultAsync(c => c.Id == chatId);
        }

        public async Task<List<Chat>> GetUserChatsAsync(string userId)
        {
            return await _context.Chats
                .Include(c => c.User1)
                .Include(c => c.User2)
                .Where(c => c.User1Id == userId || c.User2Id == userId)
                .ToListAsync();
        }

        public async Task<Chat> CreateChatAsync(string user1Id, string user2Id)
        {
            var chat = new Chat
            {
                User1Id = user1Id,
                User2Id = user2Id,
                Messages = new List<ChatMessage>()
            };

            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();

            return chat;
        }

        public async Task SendMessageAsync(int chatId, string senderId, string content)
        {
            var message = new ChatMessage
            {
                ChatId = chatId,
                SenderId = senderId,
                Content = content,
                Timestamp = DateTime.UtcNow,
                Sender = await _context.Users.FindAsync(senderId)
            };

            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task RatePartnerAsync(string partnerId, int rating, int notificationId)
        {
            var user = await _context.Users.FindAsync(partnerId);
            if (user != null)
            {
                var ratingEntry = new PartnerRating
                {
                    UserId = partnerId,
                    Rating = rating
                };

                _context.PartnerRatings.Add(ratingEntry);

                // Обновляем уведомление
                var notification = await _context.Notifications.FindAsync(notificationId);
                if (notification != null)
                {
                    notification.IsRated = true;
                }

                await _context.SaveChangesAsync();
            }
        }



        public async Task CreateEndPracticeNotification(string senderId, string receiverId)
        {
            var sender = await _context.Users.FindAsync(senderId);
            if (sender == null)
            {
                throw new ArgumentException("Sender not found");
            }

            var notification = new Notification
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Message = $"Ваш партнер {sender.Nickname ?? sender.UserName} инициировал завершение практики.",
                HasPartnerInitiatedEnd = true
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteChatAsync(int chatId)
        {
            var chat = await _context.Chats.FindAsync(chatId);
            if (chat != null)
            {
                _context.Chats.Remove(chat);
                await _context.SaveChangesAsync();
            }
        }
    }
}