﻿namespace LangPrac.Data
{
    public class Language
    {
        public int LanguageId { get; set; }
        public string? LanguageName { get; set; }
        public ICollection<UserLanguage>? UserLanguages { get; set; }
    }
}
