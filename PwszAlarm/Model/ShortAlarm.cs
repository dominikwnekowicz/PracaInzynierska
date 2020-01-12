using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Newtonsoft.Json;

namespace PwszAlarm.Model
{
    public class ShortAlarm
    {
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