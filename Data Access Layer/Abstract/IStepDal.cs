using Entity_Layer.Concrete;

namespace Data_Access_Layer.Abstract
{
    public interface IStepDal : IGenericDal<Step>
    {
        List<Step> GetStepsWithTestCase(int testCaseNumber);
    }
}
