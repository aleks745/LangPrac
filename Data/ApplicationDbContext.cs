using LangPrac.Components.Pages;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Runtime.Intrinsics.X86;

namespace LangPrac.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Language> Languages { get; set; }
        public DbSet<UserLanguage> UserLanguages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<PartnerRating> PartnerRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserLanguage>()
                .HasKey(ul => new { ul.UserId, ul.LanguageId });

            modelBuilder.Entity<UserLanguage>()
                .HasOne(ul => ul.User)
                .WithMany(u => u.UserLanguages)
                .HasForeignKey(ul => ul.UserId);

            modelBuilder.Entity<UserLanguage>()
                .HasOne(ul => ul.Language)
                .WithMany(l => l.UserLanguages)
                .HasForeignKey(ul => ul.LanguageId);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Sender)
                .WithMany()
                .HasForeignKey(n => n.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Receiver)
                .WithMany()
                .HasForeignKey(n => n.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);


            // ���� ��� ������ � ����� ������������� (User1)
            // ������������(User1) ����� ����� ��������� �����
            // ������� ���� User1Id � ������� Chats ��������� �� Id � ������� AspNetUsers
            // ��� �������� ������������(User1) ��������� ���� �� ����� ������� (DeleteBehavior.Restrict)
            modelBuilder.Entity<Chat>()
                .HasOne(c => c.User1)
                .WithMany()
                .HasForeignKey(c => c.User1Id)
                .OnDelete(DeleteBehavior.Restrict);

            // ���������� ��� User2
            modelBuilder.Entity<Chat>()
                .HasOne(c => c.User2)
                .WithMany()
                .HasForeignKey(c => c.User2Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chat>()
                .Property(c => c.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            // ���� ��������� ������� � ����� �����
            // ��� ����� ��������� ��������� ���������
            // ������� ���� ChatId � ������� ChatMessages ��������� �� Id � ������� Chats
            modelBuilder.Entity<ChatMessage>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId);

            // ���� ��������� ������� � ����� ������������� (������������)
            // ������������(�����������) ����� ��������� ��������� ���������
            // ������� ���� SenderId � ������� ChatMessages ��������� �� Id � ������� AspNetUsers
            modelBuilder.Entity<ChatMessage>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId);

            modelBuilder.Entity<PartnerRating>()
                .HasOne(pr => pr.User)
                .WithMany(u => u.UserRatings)
                .HasForeignKey(pr => pr.UserId);
        }
    }
}
