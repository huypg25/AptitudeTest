using AptidudeTest.Data.Base;
using AptidudeTest.Models;

namespace AptidudeTest.Data.Services
{
    public interface IExamRepository : IGenericRepository<Exam>
    {
        public Exam GetExamById(int id);
        public IEnumerable<Exam> GetExamsDetails();

    }
}
