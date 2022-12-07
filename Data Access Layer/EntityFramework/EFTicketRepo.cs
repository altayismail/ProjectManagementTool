using Data_Access_Layer.Abstract;
using Data_Access_Layer.Concrete;
using Data_Access_Layer.Repostories;
using Entity_Layer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.EntityFramework
{
    public class EFTicketRepo : GenericRepository<Ticket>, ITicketDal
    {
        public List<Ticket> GetAllTicketWithColumnAndAssignee()
        {
            using (var context = new Context())
            {
                return context.Tickets
                    .Include(x => x.Assigned)
                    .Include(x => x.Column)
                    .ToList();
            }
        }
    }
}
