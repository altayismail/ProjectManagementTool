namespace Business_Layer.Abstract
{
    internal interface IGenericService<T>
    {
        void AddT(T t);
        void DeleteT(T t);
        void UpdateT(T t);
        List<T> GetAllQuery();
        T GetQueryById(int id);
    }
}
