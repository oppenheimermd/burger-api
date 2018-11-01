using System.ComponentModel.DataAnnotations;

namespace BurgerApi.Models
{
    /// <summary>
    /// Burger cuisine 
    /// </summary>
    public class Cuisine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(4), MinLength(4)]
        public string CuisineCode { get; set; }

        [Required]
        public string CuisineTitle { get; set; }
    }
}
