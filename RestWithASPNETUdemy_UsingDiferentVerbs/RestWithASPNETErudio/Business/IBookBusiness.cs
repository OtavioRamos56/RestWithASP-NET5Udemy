using RestWithASPNETErudio.Model;

namespace RestWithASPNETErudio.Business
{
    public interface IBookBusiness
    {
        List<Book> FindAll();
        Book Create(Book book);
    }
}
