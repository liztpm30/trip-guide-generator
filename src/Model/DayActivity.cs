using Newtonsoft.Json;

namespace trip_guide_generator.Model
{
    public class DayActivity
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "actName")]
        public string ActName { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "instagramLink")]
        public string InstagramLink { get; set; }
    }
}

