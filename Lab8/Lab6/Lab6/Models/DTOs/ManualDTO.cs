namespace Lab6.Models.DTOs
{
    public class ManualDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid TeacherId { get; set; }
    }
}
