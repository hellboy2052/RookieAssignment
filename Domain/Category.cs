using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName="nvarchar(50)")]
        public string Name { get; set; }

        public ICollection<CategoryProduct> ProductCategories { get; set; } = new List<CategoryProduct>();
    }
}