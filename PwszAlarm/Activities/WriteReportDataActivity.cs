using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using PwszAlarm.Model;
using PwszAlarm.PwszAlarmDB;

namespace PwszAlarm.Activities
{
    [Activity(Label = "WriteReportDataActivity")]
    public class WriteReportDataActivity : Activity
    {
        TextView roomTextView;
        TextView floorTextView;
        EditText nameEditText;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.WriteReportData);
            Button okButton = FindViewById<Button>(Resource.Id.acceptButton);
            nameEditText = FindViewById<EditText>(Resource.Id.nameEditText);
            roomTextView = FindViewById<TextView>(Resource.Id.roomTextView);
            floorTextView = FindViewById<TextView>(Resource.Id.floorTextView);

            roomTextView.Text = Intent.GetStringExtra("choosenRoom");
            floorTextView.Text = Intent.GetStringExtra("choosenFloor");
            // Create your application here
            okButton.Click += OkButton_Click;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            InputMethodManager imm = (InputMethodManager)GetSystemService(InputMethodService);
            imm.HideSoftInputFromWindow(nameEditText.WindowToken, 0);
            ShortAlarm shortAlarm = new ShortAlarm();
            Room room = SQLiteDb.FindRoom(roomTextView.Text);
            string now = DateTime.Now.ToString("s");
            shortAlarm.Archived = false;
            shortAlarm.RoomId = room.Id;
            shortAlarm.Name = nameEditText.Text;
            shortAlarm.UserEmail = "dominik.wnekowicz@gmail.com";
            shortAlarm.NotifyDate = Convert.ToDateTime(now);
            _ = WebApiDataController.PostAlarm(this, shortAlarm);
            Task.Delay(3000);
            Finish();
        }
    }
}