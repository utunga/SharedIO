using System;
using System.Collections.Generic;

namespace SharedIO.Model
{
    public class Transaction
    {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public string PayerId { get; set; }
        public string PayeeId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public List<Category> Categories { get; set; }
    }
}
