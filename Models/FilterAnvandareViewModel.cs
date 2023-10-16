using System.ComponentModel.DataAnnotations;

public class FilterAnvandareViewModel
{
    [Display(Name = "Filtrera användare över ålder")]
    public int FilterAge { get; set; }
}
