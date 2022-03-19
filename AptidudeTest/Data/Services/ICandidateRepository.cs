using AptidudeTest.Data.Base;
using AptidudeTest.Models;

namespace AptidudeTest.Data.Services
{
    public interface ICandidateRepository : IGenericRepository<ApplicationUser>
    {
        ApplicationUser GetByEmail(string email);

    }
}
