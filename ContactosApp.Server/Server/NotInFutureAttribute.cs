using System.ComponentModel.DataAnnotations;

namespace ContactosApp.Server.Server
{
    public class NotInFutureAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date.Date > DateTime.Today)
                {
                    return new ValidationResult(ErrorMessage ?? "La fecha no puede estar en el futuro.");
                }
            }
            return ValidationResult.Success;
        }

    }
}
