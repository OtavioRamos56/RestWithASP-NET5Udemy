using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestWithASPNETErudio.Model;
using RestWithASPNETErudio.Model.Context;
using RestWithASPNETErudio.Repository;

namespace RestWithASPNETErudio.Business.Implementations
{
    public class BooksBusinessImplementation : IBookBusiness
    {
        private readonly IBooksRepository _repository;

        public BooksBusinessImplementation(IBooksRepository repository)
        {
            _repository = repository;
        }
        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }
        public Book Create(Book book)
        {
            return _repository.Create(book);
        }
}
}
