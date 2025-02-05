using System.ComponentModel.DataAnnotations;

namespace BlogApi.ViewModels
{
    public class EditorCategoryViewModel
    {
        [Required(ErrorMessage = "Name is required!")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "The Name must be between 3 and 40 characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Slug is required!")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "The Slug must be between 3 and 80 characters!")]
        public string Slug { get; set; }
    }
}
