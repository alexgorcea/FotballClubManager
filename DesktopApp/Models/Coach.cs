using System.ComponentModel.DataAnnotations;

namespace DesktopApp.Models
{
    public class Coach
    {
        public int CoachId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name can't be longer than 50 characters")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name can't be longer than 50 characters")]
        public string LastName { get; set; }

        public String FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Display(Name = "Team")]
        public int? TeamId { get; set; }
        public Team? Team { get; set; }
        public ICollection<TrainingSession>? TrainingSessions { get; set; }
    }

}
