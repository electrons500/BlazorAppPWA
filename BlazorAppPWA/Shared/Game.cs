using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlazorAppPWA.Shared
{
    public class Game
    {
        [DisplayName("Game Id")]
        public int Id { get; set; }
        [Required]
        [DisplayName("Game Name")]
        public string GameName { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; } = 0.00M;

        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
