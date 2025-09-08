using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Api.Validations;

public sealed class CpfAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext _)
    {
        var raw = value?.ToString() ?? "";
        var digits = Regex.Replace(raw, @"\D", "");
        if (digits.Length != 11) return new("CPF deve ter 11 dígitos.");
        if (new string(digits[0], 11) == digits) return new("CPF inválido.");

        int Sum(int len, int startWeight)
        {
            var s = 0; for (int i = 0; i < len; i++) s += (digits[i] - '0') * (startWeight - i);
            return s;
        }
        int d1 = (Sum(9, 10) * 10) % 11; if (d1 == 10) d1 = 0;
        int d2 = (Sum(10, 11) * 10) % 11; if (d2 == 10) d2 = 0;

        return (digits[9] - '0') == d1 && (digits[10] - '0') == d2
            ? ValidationResult.Success
            : new ValidationResult("CPF inválido.");
    }
}
