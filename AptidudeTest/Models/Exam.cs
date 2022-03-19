using System.ComponentModel.DataAnnotations;

namespace AptidudeTest.Models
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Exam Name")]
        [Required(ErrorMessage = "Exam Name is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Exam Name must be between 3 and 255 chars")]
        public string ExamName { get; set; }
        [Display(Name = "Pass Score")]
        [Required(ErrorMessage = "Pass Score is required")]
        public double PassScore { get; set; }
        public int Time { get; set; }
        public int Status { get; set; } = 1;
        public virtual List<Question> Questions { get; set; }
        public List<Result> Results { get; set; }
    }
}
