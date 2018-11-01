using System.ComponentModel.DataAnnotations;

namespace BurgerApi.Models
{
    /// <summary>
    /// Burger base for this burger, i.e. Beef, Vegetarian, Turkey...
    /// </summary>
    public class BurgerBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string BaseName { get; set; }

        [Required]
        [MaxLength(4), MinLength(4)]
        public string BaseNameCode { get; set; }
    }

}
