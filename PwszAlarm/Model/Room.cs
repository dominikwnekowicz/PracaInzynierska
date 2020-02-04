using Newtonsoft.Json;

namespace PwszAlarm.Model
{
    public class Room
    {
        [JsonProperty (PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty (PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty (PropertyName = "floor")]
        public string Floor { get; set; }

        [JsonProperty (PropertyName = "side")]
        public string Side { get; set; }
    }
}