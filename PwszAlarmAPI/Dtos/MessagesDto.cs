using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PwszAlarmAPI.Dtos
{
    public class MessagesDto
    {
        public int Id { get; set; }
        public int AlarmId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public DateTime MessageTime { get; set; }
    }
}