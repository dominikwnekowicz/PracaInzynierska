using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PwszAlarm.Model
{
    public class Messages
    {
        public int Id { get; set; }
        public int AlarmId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public DateTime MessageTime { get; set; }
    }
    public class SendMessage
    {
        public int AlarmId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
    }
}