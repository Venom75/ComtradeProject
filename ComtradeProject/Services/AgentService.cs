using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.IdentityModel.Tokens;
using ComtradeProject.DTOs;
using ComtradeProject.Model;
using ComtradeProject.Repository;
using ServiceReference;
using System.Formats.Asn1;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ComtradeProject.Services
{
    public class AgentService : IAgentService
    {
        private readonly IMapper _mapper;
        private readonly IAgentRepository _agentRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IConfigurationSection _secretKey;
        private readonly SOAPDemoSoapClient _soapClient;

        public AgentService(IAgentRepository agentRepository, IConfiguration configuration, IMapper mapper, SOAPDemoSoapClient soapClient, IPersonRepository personRepository)
        {
            _agentRepository = agentRepository;
            _secretKey = configuration.GetSection("SecretKey");
            _mapper = mapper;
            _soapClient = soapClient;
            _personRepository = personRepository;
        }
        public async Task<PersonDTO> AddReward(string personId, int agentId, string reward)
        {
            var agent = await _agentRepository.GetById(agentId);
            var person = await _soapClient.FindPersonAsync(personId);
            var personrep = await _personRepository.GetById(Int32.Parse(personId));

            if (personrep != null)
            {
                throw new Exception("You can not add two rewards for same Person!");
            }
            if (agent.LastReward == null || agent.FirstReward == null || (agent.LastReward.Value - agent.FirstReward.Value).TotalHours >= 24)
            {
                agent.FirstReward = DateTime.Now;
                agent.RewardCount = 0;
            }

            if (agent.RewardCount < 5)
            {
                agent.LastReward = DateTime.Now;
                agent.RewardCount++;
            }
            else
            {
                throw new Exception("You do not have more rewards for today!");
            }

            _agentRepository.Update(agent);

            var updatedPerson = _mapper.Map<Model.Person>(person);
            updatedPerson.Reward = reward;
            updatedPerson.PersonId = Int32.Parse(personId);
            await _personRepository.Insert(updatedPerson);

            var personDTO = _mapper.Map<PersonDTO>(updatedPerson);
            personDTO.AgentRewards = agent.RewardCount;
            return personDTO;
        }

        public int GetUserId(ClaimsPrincipal claim)
        {
            return int.Parse(claim.Claims.First(i => i.Type == "ID").Value);
        }

        public async Task<LoginResponseDTO> Login(LoginDTO agent)
        {
            var agent1 = await _agentRepository.GetByUsername(agent.Username);
            if (agent1 == null)
            {
                throw new Exception("Agent with these credentials does not exist!");
            }
            if (!BCrypt.Net.BCrypt.Verify(agent.Password, agent1.Password))
            {
                throw new Exception("Wrong password!");
            }

            List<Claim> userClaims = new List<Claim>();
            userClaims.Add(new Claim("ID", agent1.AgentId.ToString()));
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
            var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:43944",
                claims: userClaims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: signinCredentials
                );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new LoginResponseDTO() { Token = tokenString, RewardCount = agent1.RewardCount };
        }

        public async Task<RegisterResponseDTO> Register(RegisterDTO register)
        {
            var user = await _agentRepository.GetByUsername(register.Username);
            if (user != null)
            {
                throw new Exception("User with this email already exist!");
            }

            Agent newUser = _mapper.Map<Agent>(register);
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

            await _agentRepository.Insert(newUser);
            return _mapper.Map<RegisterResponseDTO>(newUser);
        }

        public async Task<string> WriteToCsv()
        {
            var fileName = "report.csv";
            var reportFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports");
            var filePath = Path.Combine(reportFolderPath, fileName);
            List<Model.Person> records = await _personRepository.GetAll();
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecords(records);
            }
            return "Report successfully created!";
        }
    }
}
