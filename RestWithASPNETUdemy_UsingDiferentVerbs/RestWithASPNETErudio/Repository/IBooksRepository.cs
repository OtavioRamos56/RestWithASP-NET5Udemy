using RestWithASPNETErudio.Model;

namespace RestWithASPNETErudio.Repository
{
    public interface IBooksRepository
    {
        List<Book> FindAll();
    }
}

