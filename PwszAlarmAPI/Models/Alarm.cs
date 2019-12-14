using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PwszAlarmAPI.Models
{
    public class Alarm
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public DateTime NotifyDate { get; set; }
        public bool Accepted { get; set; }
    }
}