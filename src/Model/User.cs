using System;
namespace trip_guide_generator.Model
{
    using Newtonsoft.Json;

    public class User
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "guides")]
        public Guide[] Guides { get; set; }
    }
}

