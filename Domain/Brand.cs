using System.ComponentModel.DataAnnotations;

namespace API.models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}