namespace LangPrac.Data
{
    public class UserLanguage
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public string LanguageLvl { get; set; }
    }
}
