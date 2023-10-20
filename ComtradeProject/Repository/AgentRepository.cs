using Microsoft.EntityFrameworkCore;
using ComtradeProject.Database;
using ComtradeProject.Model;

namespace ComtradeProject.Repository
{
    public class AgentRepository : IAgentRepository
    {
        private readonly DBContext _dbContext;
        public AgentRepository(DBContext dBContext) 
        {
            _dbContext = dBContext;
        }

        public async Task<Agent> GetById(int id)
        {
            return await _dbContext.Agents.FirstOrDefaultAsync(u => u.AgentId == id);
        }

        public async Task<Agent> GetByUsername(string username)
        {
            return await _dbContext.Agents.FirstOrDefaultAsync(u => u.Username.Equals(username));
        }
        public async Task<Agent> Insert(Agent agent)
        {
            await _dbContext.Agents.AddAsync(agent);
            await _dbContext.SaveChangesAsync();
            return agent;
        }

        public void Update(Agent agent)
        {
            Agent newAgent = _dbContext.Agents.FirstOrDefault(x => x.AgentId == agent.AgentId);
            newAgent = agent;
            _dbContext.Agents.Update(newAgent);
            _dbContext.SaveChanges();
        }
    }
}
