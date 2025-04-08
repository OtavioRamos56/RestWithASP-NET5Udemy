using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestWithASPNETErudio.Model;
using RestWithASPNETErudio.Model.Context;
using RestWithASPNETErudio.Repository;

namespace RestWithASPNETErudio.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;

        public PersonBusinessImplementation(IPersonRepository repository)
            {
                _repository = repository;
            }
        public List<Person> FindAll()
            {
                return _repository.FindAll();
            }
        public Person FindById(long id)
        {
            return _repository.FindById(id);
        }
        public Person Create(Person person)
        {
            
            return _repository.Create(person);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }


        public Person Update(Person person)
        {
            return _repository.Update(person);
        }
    }
}
