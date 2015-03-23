using System;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace SharedIO
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseWelcomePage();
            app.UseMvc();
        }

        [Required]
        public string tmp { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
        }
    }
}
