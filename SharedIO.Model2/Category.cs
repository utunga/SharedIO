using System;

namespace SharedIO.Model
{
    public class Category
    {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string Description { get; set; }
    }
}