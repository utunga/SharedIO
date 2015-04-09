using System.ComponentModel.DataAnnotations;

namespace SharedIO.Model
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }
}