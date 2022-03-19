using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AptidudeTest.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; } = string.Empty;
        [Required]
        public string Text { get; set; }
        [Required]
        public float Point { get; set; }
        public int ExamId { get; set; }
        [ForeignKey("ExamId")]
        public Exam Exam { get; set; }
        public virtual List<Choice> Choices { get; set; }
    }
}
