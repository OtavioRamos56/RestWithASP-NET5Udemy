using RestWithASPNETErudio.Model.Base;

namespace RestWithASPNETErudio.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);

        T FindById(long id);

        List<T> FindAll();

        T Update(T item);

        void Delete(long id);

        bool Exists(long id);
        List<T> FindWithPagedSearch(string query);

        int GetCount(string query);
    }
}
