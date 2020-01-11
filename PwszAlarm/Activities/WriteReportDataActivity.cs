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

namespace PwszAlarm
{
    [Activity(Label = "WriteReportDataActivity")]
    public class WriteReportDataActivity : Activity
    {
        TextView roomTextView;
        TextView floorTextView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.WriteReportData);
            Button okButton = FindViewById<Button>(Resource.Id.acceptButton);
            EditText notesEditText = FindViewById<EditText>(Resource.Id.notesEditText);
            roomTextView = FindViewById<TextView>(Resource.Id.roomTextView);
            floorTextView = FindViewById<TextView>(Resource.Id.floorTextView);

            roomTextView.Text = Intent.GetStringExtra("choosenRoom");
            floorTextView.Text = Intent.GetStringExtra("choosenFloor");
            // Create your application here
            okButton.Click += OkButton_Click;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Alarm alarm = new Alarm();

            alarm.Accepted = false;
            alarm.RoomId = 1;
            alarm.Notes = 
            alarm.UserEmail = "dominik.wnekowicz@gmail.com";
            alarm.NotifyDate = DateTime.Now;
            WebApiDataController.PostAlarm(this, alarm);
        }
    }
}