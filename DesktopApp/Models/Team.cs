using System.ComponentModel.DataAnnotations;

namespace DesktopApp.Models
{
    public class Team
    {
        public int TeamId { get; set; }

        [Required(ErrorMessage = "Team Name is required")]
        [StringLength(100, ErrorMessage = "Team Name can't be longer than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Division is required")]
        [StringLength(50, ErrorMessage = "Division can't be longer than 50 characters")]
        public string Division { get; set; }

        public Coach? Coach { get; set; }
        public ICollection<Player>? Players { get; set; }
    }

}
