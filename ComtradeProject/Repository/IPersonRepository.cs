using ComtradeProject.Model;

namespace ComtradeProject.Repository
{
    public interface IPersonRepository
    {
        Task<Person> Insert(Person person);
        void Update(Person person);
        Task<List<Person>> GetAll();
        Task<Person> GetById(int id);
    }
}
