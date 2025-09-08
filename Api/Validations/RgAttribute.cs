using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Api.Validations;

public sealed class RgAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext _)
    {
        var raw = value?.ToString() ?? "";
        if (string.IsNullOrWhiteSpace(raw)) return ValidationResult.Success;

        var normalized = Regex.Replace(raw, @"[\.\-\/\s]", "").ToUpperInvariant();
        if (!Regex.IsMatch(normalized, @"^[0-9]{5,13}[0-9X]?$"))
            return new("RG inválido. Use 5–14 dígitos (pode terminar com 'X').");

        return ValidationResult.Success;
    }
}
