using AptidudeTest.Data.Base;
using AptidudeTest.Models;
using Microsoft.EntityFrameworkCore;

namespace AptidudeTest.Data.Services
{
    public class ExamRepository : GenericRepository<Exam>, IExamRepository
    {
        private readonly ApplicationDbContext _context;
        public ExamRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public Exam GetExamById(int id)
        {
            var examDetails = _context.Exams
                    .Include(q => q.Questions)
                    .ThenInclude(c => c.Choices)
                    .FirstOrDefault(n => n.Id == id);
            return examDetails;
        }

		public IEnumerable<Exam> GetExamsDetails()
		{
           return _context.Exams
                 .Include(q => q.Questions).ToList();

		}

		
    }
}
