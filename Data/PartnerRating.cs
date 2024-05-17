namespace LangPrac.Data
{
    public class PartnerRating
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int Rating { get; set; }
    }
}
