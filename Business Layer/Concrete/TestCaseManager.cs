using Business_Layer.Abstract;
using Data_Access_Layer.Abstract;
using Entity_Layer.Concrete;

namespace Business_Layer.Concrete
{
    public class TestCaseManager : ITestCaseService
    {
        ITestCaseDal _testCaseDal;
        public TestCaseManager(ITestCaseDal testCaseDal)
        {
            _testCaseDal = testCaseDal;
        }

        public void AddT(TestCase t)
        {
            _testCaseDal.Add(t);
        }

        public void DeleteT(TestCase t)
        {
            _testCaseDal.Delete(t);
        }

        public List<TestCase> GetAllQuery()
        {
            return _testCaseDal.GetAllQuery();
        }

        public TestCase GetQueryById(int id)
        {
            return _testCaseDal.GetQueryById(id);
        }

        public void UpdateT(TestCase t)
        {
            _testCaseDal.Update(t);
        }

        public List<TestCase> GetAllQueryWithTicket()
        {
            return _testCaseDal.GetAllQueryWithTicket();
        }
    }
}
