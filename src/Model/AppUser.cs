using System;
namespace trip_guide_generator.Model
{
    using Newtonsoft.Json;

    public class AppUser
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string? UserName { get; set; }

        [JsonProperty(PropertyName = "guides")]
        public List<Guide>? Guides { get; set; }
    }
}

