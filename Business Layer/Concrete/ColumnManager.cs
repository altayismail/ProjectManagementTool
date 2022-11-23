using Business_Layer.Abstract;
using Data_Access_Layer.Abstract;
using Entity_Layer.Concrete;

namespace Business_Layer.Concrete
{
    public class ColumnManager : IColumnService
    {
        IColumnDal _columnDal;

        public ColumnManager(IColumnDal columnDal)
        {
            _columnDal = columnDal;
        }
        public void AddT(Column t)
        {
            _columnDal.Add(t);
        }

        public void DeleteT(Column t)
        {
            _columnDal.Delete(t);
        }

        public List<Column> GetAllQuery()
        {
            return _columnDal.GetAllQuery();
        }

        public Column GetQueryById(int id)
        {
            return _columnDal.GetQueryById(id);
        }

        public void UpdateT(Column t)
        {
            _columnDal.Update(t);
        }
    }
}
