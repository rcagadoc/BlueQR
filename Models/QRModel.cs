using System.ComponentModel.DataAnnotations;
namespace BlueQR.Models
{
    public class QRModel
    {
        public string QRImageURL { get; set; }

        [Required(ErrorMessage = "Oops! You forgot to enter a URL.")]
        public string WebsiteURL { get; set; }

    }
}
