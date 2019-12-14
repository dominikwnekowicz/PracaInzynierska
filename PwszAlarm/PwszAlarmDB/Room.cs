using Newtonsoft.Json;

namespace PwszAlarm.PwszAlarmDB
{
    public class Room
    {
        [JsonProperty (PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty (PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty (PropertyName = "floor")]
        public int Floor { get; set; }
    }
}