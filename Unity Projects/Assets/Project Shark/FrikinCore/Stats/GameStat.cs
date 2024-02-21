namespace FrikinCore.Stats
{
    public class GameStat
    {
        //todo: is this needed
        public int Id { get; set; }
        public string Type { get; set; }
        public string ApiName { get; set; }
        public string SetBy { get; set; } = "Client";
        public bool IncrementOnly { get; set; }
        public int MaxChange { get; set; }
        public float MinValue { get; set; }
        public float MaxValue { get; set; }
        public float DefaultValue { get; set; } = 0;
        public bool Aggregated { get; set; }
        public string DisplayName { get; set; }
        public float AvgRate { get; set; }
    }
}
