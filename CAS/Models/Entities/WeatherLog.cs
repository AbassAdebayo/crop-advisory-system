namespace CAS.Models.Entities
{
    public class WeatherLog
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AdvisoryId { get; set; }
        public required string Location { get; set; }
        public required double Temperature { get; set; }
        public required double Humidity { get; set; }
        public required double RainChance { get; set; }
        public required double WindSpeed { get; set; }
        public required string WeatherCondition { get; set; }
        public required string WeatherAdvice { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
