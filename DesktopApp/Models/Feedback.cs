using System.ComponentModel.DataAnnotations;

namespace DesktopApp.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }

        [Display(Name = "Player")]
        public int? PlayerId { get; set; }
        public Player? Player { get; set; }

        [Display(Name = "Training Session")]
        public int? TrainingSessionId { get; set; }

        [Display(Name = "Training Session")]
        public TrainingSession? TrainingSession { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        [StringLength(500, ErrorMessage = "Comments can't be longer than 500 characters")]
        public string Comments { get; set; }
    }

}
