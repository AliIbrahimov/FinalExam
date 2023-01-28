namespace Medicio.Models
{
    public class Icon
    {
        public int Id { get; set; }
        public string? IconName { get; set; }
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

    }
}
