using System;
using System.ComponentModel.DataAnnotations;

namespace SharedIO.Model
{
    public class Identity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
    }

    
}