using System.ComponentModel.DataAnnotations;

namespace VissmaFlow.Core.Infrastructure.Attributes
{
    public sealed class IsDirectoryExistAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value is null) return new("Путь к фалу не должен быть пустым");
            if (value is not string str) return new("Путь к фалу не должен быть пустым");
            if (Directory.Exists(str)) return ValidationResult.Success;
            else return new("Некорректный путь к файлу");
        }
    }
}
