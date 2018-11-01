using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurgerApi.Models
{
    /// <summary>
    /// Burger Instance
    /// </summary>
    public class Burger
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(140)]
        public string Description { get; set; }

        public bool Verified { get; set; } = false;


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
