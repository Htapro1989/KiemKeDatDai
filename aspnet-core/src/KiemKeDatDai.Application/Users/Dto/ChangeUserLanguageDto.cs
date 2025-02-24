using System.ComponentModel.DataAnnotations;

namespace KiemKeDatDai.Users.Dto;

public class ChangeUserLanguageDto
{
    [Required]
    public string LanguageName { get; set; }
}