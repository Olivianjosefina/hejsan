using System;
using System.ComponentModel.DataAnnotations;

namespace Vinlista.Models
{
    public class Vin
    {
        public Vin() { }

        public int VinID { get; set; } = 0;

        [Required]
        public string VinNamn { get; set; } = "";
        public string VinTyp { get; set; } = "";

        [Display(Name = "Årgång: ")]
        public int? Argang { get; set; } = 0;

        public float? AlkoholHalt { get; set; } = null;
        public string Producent { get; set; } = "";

        [Required]
        public string Land { get; set; } = "";

        [Display(Name = "BildNamn")]
        public string BildNamn { get; set; } = "";

        [Required]
        public string VinFarg { get; set; } = "";

        [Required]
        public int? Pris { get; set; } = 0;
    }
}
