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

        public async Task<Chat> GetChatAsync(int chatId, string userId)
        {
            var chat = await _context.Chats
                .Include(c => c.Messages)
                .ThenInclude(m => m.Sender)
                .FirstOrDefaultAsync(c => c.Id == chatId && (c.User1Id == userId || c.User2Id == userId));

            if (chat == null)
            {
                throw new UnauthorizedAccessException("У вас немає доступу до цього чату.");
            }

            return chat;
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

        public async Task CreateChatConfirmationNotification(string senderId, string receiverId)
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
                Message = $"Ваш партнер {sender.Nickname ?? sender.UserName} подтвердил заявку и ваш чат создан.",
                Status = "Confirmed"
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
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

        public async Task SendInviteWithDetails(ApplicationUser sender, ApplicationUser receiver)
        {
            var knownLanguages = sender.UserLanguages
                .Where(ul => ul.LanguageType == "Knows")
                .Select(ul => $"{ul.Language.LanguageName} ({ul.LanguageLvl.GetEnumMemberValue()})")
                .ToList();

            var learningLanguages = sender.UserLanguages
                .Where(ul => ul.LanguageType == "Wants to Learn")
                .Select(ul => $"{ul.Language.LanguageName} ({ul.LanguageLvl.GetEnumMemberValue()})")
                .ToList();

            var userInfo = $"Возраст: {CalculateAge(sender.DateOfBirth)}, Страна: {sender.Country}, Пол: {sender.Gender}";

            // Создаем подробное уведомление для получателя
            var notificationForReceiver = new Notification
            {
                SenderId = sender.Id,
                ReceiverId = receiver.Id,
                Message = $"Вы получили приглашение на практику от {sender.Nickname ?? sender.UserName}.",
                UserLanguagesInfo = $"Знает: {string.Join(", ", knownLanguages)}; Изучает: {string.Join(", ", learningLanguages)}",
                UserInfo = userInfo,
            };

            // Создаем уведомление для отправителя
            var notificationForSender = new Notification
            {
                SenderId = sender.Id,
                ReceiverId = sender.Id,
                Message = $"Ваше приглашение {receiver.Nickname ?? receiver.UserName} на практику было отправлено.",
                Status = "Sent"
            };

            _context.Notifications.AddRange(notificationForReceiver, notificationForSender);
            await _context.SaveChangesAsync();
        }

        private int? CalculateAge(DateOnly? dateOfBirth)
        {
            if (!dateOfBirth.HasValue)
            {
                return null;
            }

            var today = DateOnly.FromDateTime(DateTime.Today);
            var age = today.Year - dateOfBirth.Value.Year;
            if (dateOfBirth.Value.AddYears(age) > today)
            {
                age--;
            }
            return age;
        }

        public async Task MarkMessagesAsReadAsync(int chatId, string userId)
        {
            var messages = await _context.ChatMessages
                .Where(m => m.ChatId == chatId && m.SenderId != userId && !m.IsRead)
                .ToListAsync();

            foreach (var message in messages)
            {
                message.IsRead = true;
            }

            await _context.SaveChangesAsync();
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
                HasPartnerInitiatedEnd = true,
                UserInfo = string.Empty // Ensure UserInfo is not null
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