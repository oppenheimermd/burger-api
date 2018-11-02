using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurgerApi.Models
{
    /// <summary>
    /// Burger Instance
    /// </summary>
    public class Burger
    {
        public Burger()
        {
            this.TimeStamp = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }

        [StringLength(140)]
        public string Description { get; set; }

        [Required]
        [StringLength(70)]
        public string InstagramUserId { get; set; }

        //  format: https://www.instagram.com/p/Bpq3bwzHkag/
        [Required]
        public string InstagramSourceUrl { get; set; }

        public bool Verified { get; set; } = false;

        public DateTime TimeStamp { get; set; }


        [Required]
        [ForeignKey("Cuisine")]
        public int CuisineId { get; set; }
        [Required]
        [ForeignKey("BurgerBase")]
        public int BurgerBaseId { get; set; }
        [Required]
        [ForeignKey("Image")]
        public int BurgerImageId { get; set; }

        public BurgerImage Image { get; set; }
        public Cuisine Cuisine { get; set; }
        public BurgerBase BurgerBase { get; set; }
    }
}
