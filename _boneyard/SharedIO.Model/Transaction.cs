using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedIO.Model
{
    public class Transaction
    {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public string PayerId { get; set; }
        public string PayeeId { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public List<Category> Categories { get; set; }
    }
}
