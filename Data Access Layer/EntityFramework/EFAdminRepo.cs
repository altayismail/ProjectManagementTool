using Data_Access_Layer.Abstract;
using Data_Access_Layer.Repostories;
using Entity_Layer.Concrete;

namespace Data_Access_Layer.EntityFramework
{
    public class EFAdminRepo : GenericRepository<Admin>, IAdminDal
    {
    }
}
