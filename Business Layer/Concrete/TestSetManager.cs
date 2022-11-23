using Business_Layer.Abstract;
using Data_Access_Layer.Abstract;
using Entity_Layer.Concrete;

namespace Business_Layer.Concrete
{
    public class TestSetManager : ITestSetService
    {
        ITestSetDal _testSetDal;
        public TestSetManager(ITestSetDal testSetDal)
        {
            _testSetDal = testSetDal;
        }

        public void AddT(TestSet t)
        {
            _testSetDal.Add(t);
        }

        public void DeleteT(TestSet t)
        {
            _testSetDal.Delete(t);
        }

        public List<TestSet> GetAllQuery()
        {
            return _testSetDal.GetAllQuery();
        }

        public TestSet GetQueryById(int id)
        {
            return _testSetDal.GetQueryById(id);
        }

        public void UpdateT(TestSet t)
        {
            _testSetDal.Update(t);
        }
    }
}
