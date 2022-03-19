using System.ComponentModel.DataAnnotations;

namespace AptidudeTest.Models
{
    public class PassedCandidate
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; } 
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Education { get; set; }
        public string WorkExperience { get; set; } 
    }
}
