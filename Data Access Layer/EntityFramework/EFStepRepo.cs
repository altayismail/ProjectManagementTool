using Data_Access_Layer.Abstract;
using Data_Access_Layer.Concrete;
using Data_Access_Layer.Repostories;
using Entity_Layer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.EntityFramework
{
    public class EFStepRepo : GenericRepository<Step>, IStepDal
    {
        public List<Step> GetStepsWithTestCase(int testCaseNumber)
        {
            using(var context = new Context())
            {
                return context.Steps.Where(x => x.TestCaseId == testCaseNumber)
                    .Include(x => x.TestCase).ToList();
            }
        }
    }
}
