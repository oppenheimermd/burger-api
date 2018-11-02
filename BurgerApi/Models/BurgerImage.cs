using System;
using System.ComponentModel.DataAnnotations;

namespace BurgerApi.Models
{
    /// <summary>
    /// Burger Image
    /// </summary>
    public class BurgerImage
    {
        public BurgerImage()
        {
            this.TimeStamp = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string OriginalFileName { get; set; }

        [Required]
        public string ImageSourceUrl { get; set; }

        public DateTime TimeStamp { get; set; }

        public string BurgerPhotoUrl()
        {
            return $"/BurgerImages/{FileName}";
        }
    }
}
