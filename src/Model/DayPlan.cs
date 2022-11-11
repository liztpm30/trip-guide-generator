using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace trip_guide_generator.Model
{
	public class DayPlan
	{
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "DayNumber")]
        public int DayNumber { get; set; }

        [JsonProperty(PropertyName = "Activities")]
        public List<Activity>? Activities { get; set; }
    }
}

