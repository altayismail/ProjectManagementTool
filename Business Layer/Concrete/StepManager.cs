using Business_Layer.Abstract;
using Data_Access_Layer.Abstract;
using Entity_Layer.Concrete;


namespace Business_Layer.Concrete
{
    public class StepManager : IStepService
    {
        IStepDal _stepDal;
        public StepManager(IStepDal stepDal)
        {
            _stepDal = stepDal;
        }

        public void AddT(Step t)
        {
            _stepDal.Add(t);
        }

        public void DeleteT(Step t)
        {
            _stepDal.Delete(t);
        }

        public List<Step> GetAllQuery()
        {
            return _stepDal.GetAllQuery();
        }

        public Step GetQueryById(int id)
        {
            return _stepDal.GetQueryById(id);
        }

        public void UpdateT(Step t)
        {
            _stepDal.Update(t);
        }
    }
}
