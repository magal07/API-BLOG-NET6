using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels;

public class EditorCategoryViewModel
{
    [Required(ErrorMessage = "Category Name is required")]
    [StringLength(40, MinimumLength = 3, ErrorMessage = "Category Name must be between 3 and 40 characters")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Slug is required")]
    public string Slug { get; set; }    
}