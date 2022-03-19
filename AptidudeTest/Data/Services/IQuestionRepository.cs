using AptidudeTest.Data.Base;
using AptidudeTest.Models;

namespace AptidudeTest.Data.Services
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        List<Choice> GetAllChoicesByQuestionId(int id);

    }
}
