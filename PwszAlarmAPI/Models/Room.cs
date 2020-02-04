using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PwszAlarmAPI.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Floor { get; set; }
        public string Side { get; set; }
    }
}