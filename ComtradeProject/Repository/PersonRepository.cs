using Microsoft.EntityFrameworkCore;
using ComtradeProject.Database;
using ComtradeProject.Model;

namespace ComtradeProject.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DBContext _dbContext;
        public PersonRepository(DBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<List<Person>> GetAll()
        {
            return await _dbContext.Persons.ToListAsync();
        }

        public async Task<Person> Insert(Person person)
        {
            await _dbContext.Persons.AddAsync(person);
            await _dbContext.SaveChangesAsync();
            return person;
        }

        public void Update(Person person)
        {
            Person newPerson = _dbContext.Persons.FirstOrDefault(x => x.PersonId == person.PersonId);
            newPerson = person;
            _dbContext.Persons.Update(newPerson);
            _dbContext.SaveChanges();
        }
    }
}
