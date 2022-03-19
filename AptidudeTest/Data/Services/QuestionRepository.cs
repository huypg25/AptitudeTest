using AptidudeTest.Data.Base;
using AptidudeTest.Models;

namespace AptidudeTest.Data.Services
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public QuestionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public List<Choice> GetAllChoicesByQuestionId(int id)
        {
            var choices = _context.Choices.Where(x => x.QuestionId == id).ToList();
            return choices;
        }

    }
}
