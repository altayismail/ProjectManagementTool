using Business_Layer.Abstract;
using Data_Access_Layer.Abstract;
using Entity_Layer.Concrete;

namespace Business_Layer.Concrete
{
    public class TicketManager : ITicketService
    {
        ITicketDal _ticketDal;
        public TicketManager(ITicketDal ticketDal)
        {
            _ticketDal = ticketDal;
        }

        public void AddT(Ticket t)
        {
           _ticketDal.Add(t);   
        }

        public void DeleteT(Ticket t)
        {
            _ticketDal.Delete(t);
        }

        public List<Ticket> GetAllQuery()
        {
            return _ticketDal.GetAllQuery();
        }

        public Ticket GetQueryById(int id)
        {
            return _ticketDal.GetQueryById(id);
        }

        public void UpdateT(Ticket t)
        {
            _ticketDal.Update(t);
        }
    }
}
