using Entity_Layer.Concrete;

namespace Data_Access_Layer.Abstract
{
    public interface ITestCaseDal : IGenericDal<TestCase>
    {
        List<TestCase> GetAllQueryWithTicket();
    }
}
