using Business_Layer.Abstract;
using Data_Access_Layer.Abstract;
using Entity_Layer.Concrete;

namespace Business_Layer.Concrete
{
    public class ProjectManager : IProjectService
    {
        IProjectDal _projectDal;
        public ProjectManager(IProjectDal projectDal)
        {
            _projectDal = projectDal;
        }

        public void AddT(Project t)
        {
            throw new NotImplementedException();
        }

        public void DeleteT(Project t)
        {
            throw new NotImplementedException();
        }

        public List<Project> GetAllQuery()
        {
            return _projectDal.GetAllQuery();
        }

        public Project GetQueryById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateT(Project t)
        {
            throw new NotImplementedException();
        }
    }
}
