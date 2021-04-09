using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}