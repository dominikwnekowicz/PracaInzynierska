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
using PwszAlarm.Model;
using PwszAlarm.PwszAlarmDB;

namespace PwszAlarm.Activities
{
    public class AlarmsHistoryAdapter : BaseAdapter<Alarm>
    {
        private readonly List<Alarm> alarms;
        private readonly Activity activity;
        private class NewAlarms
        {
            public float Text1Size { get; set; }
            public float Text2Size { get; set; }
            public bool Loaded { get; set; }
        }
        NewAlarms loaded = new NewAlarms();
        public AlarmsHistoryAdapter(Activity activity, IEnumerable<Alarm> alarms)
        {
            this.alarms = alarms.ToList();
            this.activity = activity;
            loaded.Loaded = false;
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public override Alarm this [int index]
        {
            get { return alarms[index];  }
        }
        public override int Count
        {
            get { return alarms.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                view = activity.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
            }

            var alarm = alarms[position];
            var rooms = SQLiteDb.GetRooms(activity);
            TextView text1 = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            text1.Text = alarm.Name + " - Sala " + rooms.FirstOrDefault(r => r.Id == alarm.RoomId).Name;
            

            TextView text2 = view.FindViewById<TextView>(Android.Resource.Id.Text2);
            text2.Text = alarm.NotifyDate.Date.ToShortDateString() + " - " + alarm.NotifyDate.TimeOfDay.ToString();
            if (!loaded.Loaded)
            {
                loaded.Text1Size = text1.TextSize;
                loaded.Text2Size = text2.TextSize;
                loaded.Loaded = true;
            }
            if (DateTime.Now.Subtract(alarm.NotifyDate).TotalHours < 24)
            {
                view.SetBackgroundColor(Android.Graphics.Color.DarkRed);
                text1.SetTextColor(Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.White));
                text1.SetTextSize(Android.Util.ComplexUnitType.Px, (float)(loaded.Text1Size*1.2));
                text2.SetTextColor(Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.White));
                text2.SetTextSize(Android.Util.ComplexUnitType.Px, (float)(loaded.Text2Size*1.2));
            }
            else
            {
                view.SetBackgroundColor(Android.Graphics.Color.White);
                text1.SetTextColor(Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Black));
                text1.SetTextSize(Android.Util.ComplexUnitType.Px, (float)(loaded.Text1Size));
                text2.SetTextColor(Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Black));
                text2.SetTextSize(Android.Util.ComplexUnitType.Px, (float)(loaded.Text2Size));
            }


            return view;
        }
    }
}