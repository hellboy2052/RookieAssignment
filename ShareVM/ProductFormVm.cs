using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ShareVM
{
    public class ProductFormVm
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int BrandId { get; set; }

        public List<string> CategoryName { get; set; }

        public List<IFormFile> Pictures { get; set; }
    }
}