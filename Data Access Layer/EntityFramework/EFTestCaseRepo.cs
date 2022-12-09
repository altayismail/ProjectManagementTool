using Data_Access_Layer.Abstract;
using Data_Access_Layer.Concrete;
using Data_Access_Layer.Repostories;
using Entity_Layer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.EntityFramework
{
    public class EFTestCaseRepo : GenericRepository<TestCase>, ITestCaseDal
    {
        public List<TestCase> GetAllQueryWithTicket()
        {
            using(var context = new Context())
            {
                return context.TestCases
                    .Include(x => x.Ticket)
                    .ToList();
            }
        }
    }
}
