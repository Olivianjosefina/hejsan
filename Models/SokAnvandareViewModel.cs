using System.ComponentModel.DataAnnotations;

namespace VinApp.Models
{
    public class SokAnvandareViewModel
    {
        [Required(ErrorMessage = "Användarnamn är obligatoriskt.")]
        [Display(Name = "Användarnamn")]
        public string sokText { get; set; }
    }
}
