using System.Collections.Generic;
using System.Linq;
using WebApiDemo.DataProviders;
using WebApiDemo.Interfaces;
using WebApiDemo.Models;

namespace WebApiDemo.Services
{
    public class PersonService : IPersonService
    {
        private List<Person> Persons { get; set; }

        public PersonService()
        {
            int i = 0;
            Persons = PersonDataProvider.ListOf<Person>(50);
            Persons.ForEach(person =>
            {
                i++;
                person.Id = i;
            });
        }

        public IEnumerable<Person> GetAll()
        {
            return Persons;
        }

        public Person Get(int id)
        {
            return Persons.First(_ => _.Id == id);
        }

        public Person Add(Person person)
        {
            int newid = Persons.OrderBy(_ => _.Id).Last().Id + 1;
            person.Id = newid;

            Persons.Add(person);

            return person;
        }

        public void Update(int id, Person person)
        {
            Person existing = Persons.First(_ => _.Id == id);
            existing.FirstName = person.FirstName;
            existing.LastName = person.LastName;
            existing.Address = person.Address;
            existing.Age = person.Age;
            existing.City = person.City;
            existing.Email = person.Email;
            existing.Phone = person.Phone;
            existing.Title = person.Title;
        }

        public void Delete(int id)
        {
            Person existing = Persons.First(_ => _.Id == id);
            Persons.Remove(existing);
        }
    }
}
