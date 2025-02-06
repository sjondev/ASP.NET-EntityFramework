using System.ComponentModel.DataAnnotations;

namespace BlogApi.ViewModels;

public class UploadingImageViewModel
{
    [Required(ErrorMessage = "Image invalid")]
    public string Base64Image { get; set; }
}