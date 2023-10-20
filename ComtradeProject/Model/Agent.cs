namespace RankoNikolic.Model
{
    public class Agent
    {
        public int AgentId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RewardCount { get; set; }
        public DateTime? FirstReward { get; set; }
        public DateTime? LastReward { get; set; }
    }
}
