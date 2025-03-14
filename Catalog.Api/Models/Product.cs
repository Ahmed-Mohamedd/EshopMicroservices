﻿namespace Catalog.Api.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = default;
        public string Description { get; set; } = default;
        public List<string> Category { get; set; } = new();
        public string ImageFile { get; set; } = default; 
        
        public decimal Price {  get; set; }
    }
}
