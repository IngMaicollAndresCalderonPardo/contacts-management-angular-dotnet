using ContactosApp.Server.Server;
using System.ComponentModel.DataAnnotations;

namespace ContactosApp.Server.Dtos
{
    /// <summary>
    /// ContactCreateDto es solo para recibir datos del frontend; 
    /// cuando creas un contacto, copias esos valores en un Contact nuevo.
    /// </summary>
    public class ContactCreateDto
    {
        [Required, MinLength(2, ErrorMessage = "El nombre debe tener al menos 2 caracteres.")]
        public string Name { get; set; } = "";

        [Required,EmailAddress(ErrorMessage = "Formato de correo inválido.")]
        public string? Email { get; set; }

        [Required, RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe tener exactamente 10 dígitos numéricos.")]
        public string? Phone { get; set; }

        [Required, NotInFuture(ErrorMessage = "La fecha de nacimiento no puede ser en el futuro.")]
        public DateTime? BirthDate { get; set; }

        public string? Address { get; set; }

    }
}
