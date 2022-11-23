using Business_Layer.Abstract;
using Data_Access_Layer.Abstract;
using Entity_Layer.Concrete;

namespace Business_Layer.Concrete
{
    public class AdminManager : IAdminService
    {
        IAdminDal _adminDal;

        public AdminManager(IAdminDal adminDal)
        {
            _adminDal = adminDal;
        }
        public void AddT(Admin t)
        {
            _adminDal.Add(t);
        }

        public void DeleteT(Admin t)
        {
            _adminDal.Delete(t);
        }

        public List<Admin> GetAllQuery()
        {
            return _adminDal.GetAllQuery();
        }

        public Admin GetQueryById(int id)
        {
            return _adminDal.GetQueryById(id);
        }

        public void UpdateT(Admin t)
        {
            _adminDal.Update(t);
        }
    }
}
