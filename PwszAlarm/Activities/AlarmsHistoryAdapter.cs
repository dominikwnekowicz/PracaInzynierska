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

namespace PwszAlarm.Activities
{
    public class AlarmsHistoryAdapter : BaseAdapter<Alarm>
    {
        private readonly List<Alarm> alarms;
        private readonly Activity activity;

        public AlarmsHistoryAdapter(Activity activity, IEnumerable<Alarm> alarms)
        {
            this.alarms = alarms.ToList();
            this.activity = activity;
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

            TextView text1 = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            text1.Text = alarm.Name;

            TextView text2 = view.FindViewById<TextView>(Android.Resource.Id.Text2);
            text2.Text = alarm.NotifyDate.Date.ToShortDateString() + " - " + alarm.NotifyDate.TimeOfDay.ToString();

            return view;
        }
    }
}