using Entity_Layer.Concrete;

namespace Data_Access_Layer.Abstract
{
    public interface ITicketDal : IGenericDal<Ticket>
    {
        List<Ticket> GetAllTicketWithColumnAndAssignee();
    }
}
