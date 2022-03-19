using System.ComponentModel.DataAnnotations;

namespace AptidudeTest.Models
{
    public class Result
    {
        [Key]
        public int Id { get; set; }
        public double ResultScore { get; set; }
        public double PassScore { get; set; }
        public DateTime CreatedAt { get; set; } =DateTime.Now;
        [Required]
        public bool Status { get; set; }
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
    }
}
