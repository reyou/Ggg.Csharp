using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Models;

namespace WebApiDemo.DataProviders
{
    public class PersonDataProvider
    {
        public static List<T> ListOf<T>(int i)
        {
            if (typeof(T) != typeof(Person))
            {
                throw new InvalidOperationException();
            }
            List<Person> persons = new List<Person>();
            for (int j = 0; j < i; j++)
            {
                persons.Add(new Person()
                {
                    Address = Guid.NewGuid().ToString(),
                    Age = (new Random()).Next(10,50),
                    City = Guid.NewGuid().ToString(),
                    Email = Guid.NewGuid().ToString(),
                    FirstName = Guid.NewGuid().ToString(),
                    Id = j+1,
                    LastName = Guid.NewGuid().ToString(),
                    Phone = Guid.NewGuid().ToString(),
                    Title = Guid.NewGuid().ToString(),
                });
            }

            return persons as List<T>;
        }
    }
}
