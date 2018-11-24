using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string FileNameLarge { get; set; }

        [Required]
        public string FileNameMedium { get; set; }

        [Required]
        public string FileNameSmall { get; set; }


        public DateTime TimeStamp { get; }


        public string BurgerPhotoSmallUrl()
        {
            return $"/BurgerImages/{FileNameSmall}";
        }
        public string BurgerPhotoMediumUrl()
        {
            return $"/BurgerImages/{FileNameMedium}";
        }

        public string BurgerPhotoLargeUrl()
        {
            return $"/BurgerImages/{FileNameLarge}";
        }

    }
}
