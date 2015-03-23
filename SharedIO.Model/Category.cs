using System;

namespace SharedIO.Model
{
 
    public class CategoryStub
    {
        public string id { get; set; } //SPECNOTE spec implies ints but I think strings would be better here 
    }

    public class Category : CategoryStub
    {
        public DateTime created { get; set; }
        public string color { get; set; }
        public string name { get; set; }
        public string logo_url { get; set; }
        public string description { get; set; }
    }
}
