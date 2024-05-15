namespace LangPrac.Data
{
    public class Chat
    {
        public int Id { get; set; }
        public string User1Id { get; set; }
        public ApplicationUser User1 { get; set; }
        public string User2Id { get; set; }
        public ApplicationUser User2 { get; set; }
        public ICollection<ChatMessage> Messages { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}