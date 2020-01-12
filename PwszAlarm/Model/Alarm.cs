using Newtonsoft.Json;
using System;

namespace PwszAlarm.Model
{
    public class Alarm
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "roomId")]
        public int RoomId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "userEmail")]
        public string UserEmail { get; set; }

        [JsonProperty(PropertyName = "notifyDate")]
        public DateTime NotifyDate { get; set; }

        [JsonProperty(PropertyName = "archived")]
        public bool Archived { get; set; }
    }
}