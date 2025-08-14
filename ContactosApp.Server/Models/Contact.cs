namespace ContactosApp.Server.Models
{
    /// <summary>
    /// Contact es la entidad real que representa la tabla en la base de datos.
    /// </summary>
    public class Contact
    {
        public int Id { get; set; } // Autonumérico en memoria
        public string Name { get; set; } = ""; 
        public string? Email { get; set; }     
        public string? Phone { get; set; }     
        public DateTime? BirthDate { get; set; } 
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // se asigna al crear
        public DateTime? UpdatedAt { get; set; } // se asigna al actualizar
    }
}
