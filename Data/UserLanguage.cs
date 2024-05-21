using System.Runtime.Serialization;

namespace LangPrac.Data
{
    public class UserLanguage
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public LanguageLvl LanguageLvl { get; set; }
        public string LanguageType { get; set; }
    }

    public enum LanguageLvl
    {
        [EnumMember(Value = "Початковий")]
        A1,
        [EnumMember(Value = "Нижче середнього")]
        A2,
        [EnumMember(Value = "Середній")]
        B1,
        [EnumMember(Value = "Вище середнього")]
        B2,
        [EnumMember(Value = "Високий")]
        C1,
        [EnumMember(Value = "Професійний")]
        C2
    }

    public static class EnumExtensions
    {
        public static string GetEnumMemberValue(this Enum enumValue)
        {
            var enumMemberAttribute = enumValue.GetType()?
                .GetField(enumValue.ToString())?
                .GetCustomAttributes(typeof(EnumMemberAttribute), false)
                as EnumMemberAttribute[];

            return enumMemberAttribute?.Length > 0 ? enumMemberAttribute[0].Value : enumValue.ToString();
        }

        public static string GetDescription(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            var attributes = fieldInfo.GetCustomAttributes(typeof(EnumMemberAttribute), false) as EnumMemberAttribute[];
            return attributes?.Length > 0 ? $"{attributes[0].Value} ({enumValue})" : enumValue.ToString();
        }
    }
}
