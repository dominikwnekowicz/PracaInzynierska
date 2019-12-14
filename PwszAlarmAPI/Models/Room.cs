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
        public int Floor { get; set; }
    }
}