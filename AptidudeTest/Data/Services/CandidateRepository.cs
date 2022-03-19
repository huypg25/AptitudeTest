using AptidudeTest.Data.Base;
using AptidudeTest.Models;

namespace AptidudeTest.Data.Services
{
    public class CandidateRepository : GenericRepository<ApplicationUser>, ICandidateRepository
    {
        private readonly ApplicationDbContext _context;
        public CandidateRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public ApplicationUser GetByEmail(string email)
        {
            var result = _context.Users.Where(x => x.Email == email).FirstOrDefault();
            return result;
        }
    }
}
