namespace LangPrac.Data
{
    public class Notification
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }
        public string ReceiverId { get; set; }
        public ApplicationUser Receiver { get; set; }
        public string Message { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool HasPartnerInitiatedEnd { get; set; }
        public bool IsRated { get; set; }
        public string UserLanguagesInfo { get; set; } = string.Empty;
        public string UserInfo { get; set; } = string.Empty;
    }
}
