﻿using RestWithASPNETErudio.Model;
using RestWithASPNETErudio.Repository.Generic;

namespace RestWithASPNETErudio.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IRepository<Person> _repository;

        public PersonBusinessImplementation(IRepository<Person> repository)
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
