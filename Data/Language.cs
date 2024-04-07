namespace LangPrac.Data
{
    public class Language
    {
        public int LanguageId { get; set; }
        public string? LanguageName { get; set; }
        public string? LanguageType { get; set; }
        public string? LanguageLvl { get; set; }
        public ICollection<UserLanguage>? UserLanguages { get; set; }
    }
}
