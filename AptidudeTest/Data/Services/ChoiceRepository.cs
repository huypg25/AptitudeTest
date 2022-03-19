using AptidudeTest.Data.Base;
using AptidudeTest.Models;

namespace AptidudeTest.Data.Services
{
    public class ChoiceRepository : GenericRepository<Choice>, IChoiceRepository
    {
        private readonly ApplicationDbContext _context;
        public ChoiceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

      
    }
}
