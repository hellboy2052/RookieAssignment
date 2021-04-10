using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName="nvarchar(255)")]
        public string Name { get; set; }
        
        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }



        public ICollection<Rate> rate { get; set; } = new List<Rate>();

        public ICollection<CategoryProduct> ProductCategories { get; set; } = new List<CategoryProduct>();
    }
}