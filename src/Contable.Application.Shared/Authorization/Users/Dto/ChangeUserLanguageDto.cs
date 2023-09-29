using System.ComponentModel.DataAnnotations;

namespace Contable.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
