using System.ComponentModel.DataAnnotations;

namespace DotNet_EFCore.Models
{
    public class Monster
    {
        [Key]
        public int MonsterId { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public string Description { get; set; }

        // We can provide some hardcoded default values like so:
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // New Monster objects will have these values assigned
        // However, when we query existing data, CreatedAt/UpdatedAt will refer to 
        // values that are stored in the DB
    }
}
