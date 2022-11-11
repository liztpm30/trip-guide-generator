using System;
namespace trip_guide_generator.Model
{
    using Newtonsoft.Json;

    public class Guide
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "numberOfDays")]
        public int NumberOfDays { get; set; }

        [JsonProperty(PropertyName = "guideName")]
        public string? GuideName { get; set; }

        [JsonProperty(PropertyName = "planPerDay")]
        public List<DayPlan>? PlanPerDay { get; set; }
    }
}

