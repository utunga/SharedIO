using System;

namespace AssetManager.Model
{
    public class LogOnModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public String ErrorMessage { get; set; }
    }
}