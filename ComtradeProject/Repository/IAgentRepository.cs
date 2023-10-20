using ComtradeProject.Model;

namespace ComtradeProject.Repository
{
    public interface IAgentRepository
    {
        Task<Agent> GetByUsername(string username);
        Task<Agent> GetById(int id);
        Task<Agent> Insert(Agent agent);
        void Update(Agent agent);
    }
}
