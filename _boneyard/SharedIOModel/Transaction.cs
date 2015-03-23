using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedIO.Model
{
    public class Transaction
    {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        [Required]
        public string PayerId { get; set; }
        public string PayeeId { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public List<Category> Categories { get; set; }
    }
}
