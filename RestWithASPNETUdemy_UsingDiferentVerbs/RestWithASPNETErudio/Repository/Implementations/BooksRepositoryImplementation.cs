using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestWithASPNETErudio.Model;
using RestWithASPNETErudio.Model.Context;

namespace RestWithASPNETErudio.Repository.Implementations
{
    public class BooksRepositoryImplementation : IBooksRepository
    {
        private MySQLContext _context;

        public BooksRepositoryImplementation(MySQLContext context)
        {
            _context = context;
        }
        public List<Book> FindAll()
        {
            return _context.Books.ToList();
        }
        public Book Create(Book book)
        {
            try
            {
                _context.Add(book);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return book;
        }
    }
}
