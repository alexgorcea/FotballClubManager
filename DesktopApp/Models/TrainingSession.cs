using System.ComponentModel.DataAnnotations;

namespace DesktopApp.Models
{
    public class TrainingSession
    {
        public int TrainingSessionId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Scheduled Date")]
        [Required(ErrorMessage = "Scheduled Date is required")]
        public DateTime ScheduledDate { get; set; }

        [Display(Name = "Team")]
        public int? TeamId { get; set; }
        public Team? Team { get; set; }

        [Display(Name = "Coach")]
        public int? CoachId { get; set; }
        public Coach? Coach { get; set; }
        public ICollection<Feedback>? Feedbacks { get; set; }
    }

}
