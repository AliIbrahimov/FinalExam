using System.ComponentModel.DataAnnotations.Schema;

namespace Medicio.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string? Fullname { get; set; }
        public string? Position { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? FormFile { get; set; }
        public List<Icon>? Icons { get; set; }
    }
}
