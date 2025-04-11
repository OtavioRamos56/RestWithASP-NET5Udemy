using RestWithASPNETErudio.Model;

namespace RestWithASPNETErudio.Business
{
    public interface IBookBusiness
    {
        Book Create(Book book);
        Book FindById(long id);
        List<Book> FindAll();
<<<<<<< HEAD
        Book Update(Book book);
        void Delete(long id);
=======
        Book Create(Book book);
>>>>>>> bd4ec5005d7389e9ea9fd7b9f332580a24f1757f
    }
}
