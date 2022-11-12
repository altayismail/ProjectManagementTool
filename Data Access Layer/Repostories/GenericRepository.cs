using Data_Access_Layer.Abstract;
using Data_Access_Layer.Concrete;

namespace Data_Access_Layer.Repostories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        public void Add(T t)
        {
            using var context = new Context();
            context.Add(t);
            context.SaveChanges();
        }

        public void Delete(T t)
        {
            using var context = new Context();
            context.Remove(t);
            context.SaveChanges();
        }

        public List<T> GetAllQuery()
        {
            using var context = new Context();
            return context.Set<T>().ToList();
        }

        public T GetQueryById(int id)
        {
            using var context = new Context();
            return context.Set<T>().Find(id);
        }

        public void Update(T t)
        {
            using var context = new Context();
            context.Update(t);
            context.SaveChanges();
        }

        public void AddRange(List<T> t)
        {
            using var context = new Context();
            context.AddRange(t);
            context.SaveChanges();
        }
    }
}
