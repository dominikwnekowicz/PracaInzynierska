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

        [JsonProperty(PropertyName = "notes")]
        public string Notes { get; set; }

        [JsonProperty(PropertyName = "userEmail")]
        public string UserEmail { get; set; }

        [JsonProperty(PropertyName = "notifyDate")]
        public DateTime NotifyDate { get; set; }

        [JsonProperty(PropertyName = "accepted")]
        public bool Accepted { get; set; }
    }
}