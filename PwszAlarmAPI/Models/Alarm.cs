using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PwszAlarmAPI.Models
{
    public class Alarm
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public DateTime NotifyDate { get; set; }
        public bool Archived { get; set; }
    }
}