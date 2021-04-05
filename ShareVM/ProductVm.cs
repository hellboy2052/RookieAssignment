using System.Collections.Generic;

namespace ShareVM
{
    public class ProductVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string BrandName { get; set; }

        public ICollection<CategoryVm> ProductCategories { get; set; }
    }
}