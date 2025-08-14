namespace ContactosApp.Server.Dtos
{
    /// <summary>
    /// ContactDto es solo para enviar datos al frontend; 
    /// cuando devuelves datos, tomas la entidad Contact y creas un ContactDto.
    /// </summary>
    public class ContactDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
