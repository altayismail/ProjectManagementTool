using Business_Layer.Abstract;
using Data_Access_Layer.Abstract;
using Entity_Layer.Concrete;
using System.Web.Mvc;

namespace Business_Layer.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void AddT(User t)
        {
            _userDal.Add(t);
        }

        public void DeleteT(User t)
        {
            _userDal.Delete(t);
        }

        public List<User> GetAllQuery()
        {
            return _userDal.GetAllQuery(); 
        }

        public User GetQueryById(int id)
        {
            return _userDal.GetQueryById(id);
        }

        public void UpdateT(User t)
        {
            _userDal.Update(t);
        }
    }
}
