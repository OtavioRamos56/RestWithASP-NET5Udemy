using RestWithASPNETErudio.Data.Converter.Contract;
using RestWithASPNETErudio.Data.VO;
using RestWithASPNETErudio.Model;

namespace RestWithASPNETErudio.Data.Converter.Implementations
{
    public class BookConverter : IParser<BookVO, Book>, IParser<Book, BookVO>
    {
        public Book Parse(BookVO origin)
        {
            if (origin == null) 
            return null;
            return new Book
            {
                Id = origin.Id,
                Title = origin.Title,
                Author = origin.Author,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price
            };
        }
        public BookVO Parse(Book origin)
        {
            if (origin == null) 
            return null;
            return new BookVO
            {
                Id = origin.Id,
                Title = origin.Title,
                Author = origin.Author,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price
            };
        } 
        public List<Book> Parse(List<BookVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(Item => Parse(Item)).ToList();
        }



        public List<BookVO> Parse(List<Book> origin)
        {
            if (origin == null) return null;
            return origin.Select(Item => Parse(Item)).ToList();
        }
    }
}
