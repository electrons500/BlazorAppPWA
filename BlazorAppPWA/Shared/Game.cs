using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorAppPWA.Shared
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        public string GameName { get; set; } = null!;

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
