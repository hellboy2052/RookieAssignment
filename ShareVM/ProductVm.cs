using System;
using System.Collections.Generic;

namespace ShareVM
{
    public class ProductVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }


        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string BrandName { get; set; }

        public int ratingCount { get; set; }

        public double rating { get; set; }

        public string currentRate { get; set; }

        public bool IsRate { get; set; }

        public ICollection<RateVm> rate { get; set; } = new List<RateVm>();

        public ICollection<CategoryVm> ProductCategories { get; set; }

        public ICollection<PictureVm> Pictures { get; set; }
    }
}