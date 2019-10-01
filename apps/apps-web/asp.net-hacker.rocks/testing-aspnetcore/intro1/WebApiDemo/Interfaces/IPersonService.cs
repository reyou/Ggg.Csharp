using System.Collections.Generic;
using WebApiDemo.Models;

namespace WebApiDemo.Interfaces
{
    public interface IPersonService
    {
        IEnumerable<Person> GetAll();
        Person Get(int id);
        Person Add(Person person);
        void Update(int id, Person person);
        void Delete(int id);
    }
}