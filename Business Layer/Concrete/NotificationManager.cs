using Business_Layer.Abstract;
using Data_Access_Layer.Abstract;
using Entity_Layer.Concrete;

namespace Business_Layer.Concrete
{
    public class NotificationManager : INotificationService
    {
        INotificationDal? _notificationDal;
        public NotificationManager(INotificationDal? notificationDal)
        {
            _notificationDal = notificationDal;
        }

        public void AddT(Notification t)
        {
            _notificationDal.Add(t);
        }

        public void DeleteT(Notification t)
        {
            _notificationDal.Delete(t);
        }

        public List<Notification> GetAllQuery()
        {
            return _notificationDal.GetAllQuery();
        }

        public Notification GetQueryById(int id)
        {
            return _notificationDal.GetQueryById(id);
        }

        public void UpdateT(Notification t)
        {
            _notificationDal.Update(t);
        }
    }
}
