using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestWithASPNETErudio.Model;
using RestWithASPNETErudio.Model.Context;

namespace RestWithASPNETErudio.Repository.Implementations
{
    public class PersonRepositoryImplementation : IPersonRepository
    {
        private MySQLContext _context;

        public PersonRepositoryImplementation(MySQLContext context)
            {
                _context = context;
            }
        public List<Person> FindAll()
            {
                return _context.Persons.ToList();
            }
        public Person FindById(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
        }
        public Person Create(Person person)
        {
             try
             {
                 _context.Add(person);
                 _context.SaveChanges();
             }
                 catch (Exception)
             {
                  throw;
             }
            return person;
        }

        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {

            }
            try
            {
                _context.Persons.Remove(result);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Person Update(Person person)
        {
            if (!Exists(person.Id)) return new Person();
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));

                if (result != null)
                {

                }
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
                return person;
            }

        public bool Exists(long id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id)); ;
        }
    }
}
