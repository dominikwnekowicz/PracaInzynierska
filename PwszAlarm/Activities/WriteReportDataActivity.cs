using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using PwszAlarm.Model;
using PwszAlarm.PwszAlarmDB;

namespace PwszAlarm.Activities
{
    [Activity(Label = "WriteReportDataActivity")]
    public class WriteReportDataActivity : AppCompatActivity
    {
        EditText nameEditText;
        string choosenRoom, choosenFloor;

        public override void OnBackPressed()
        {
            var intent = new Intent(this, typeof(ReportEmergencyActivity));
            StartActivity(intent);
            Finish();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.WriteReportData);
            Button okButton = FindViewById<Button>(Resource.Id.acceptButton);
            nameEditText = FindViewById<EditText>(Resource.Id.nameEditText);
            Button cancelButton = FindViewById<Button>(Resource.Id.cancelButton);
            // Create your application here
            
            choosenRoom = Intent.GetStringExtra("choosenRoom");
            choosenFloor = Intent.GetStringExtra("choosenFloor");

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.writeReportDataToolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Piętro " + choosenFloor + " - Sala" + choosenRoom;

            okButton.Click += OkButton_Click;
            cancelButton.Click += CancelButton_Click;
            nameEditText.EditorAction += (sender, e) =>
            {
                if (e.ActionId == ImeAction.Done)
                {
                    okButton.PerformClick();
                }
                else
                {
                    e.Handled = false;
                }
            };
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ReportEmergencyActivity));
            StartActivity(intent);
            Finish();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            var user = SQLiteDb.GetUser();
            if (user.Email == "failed") return;
            InputMethodManager imm = (InputMethodManager)GetSystemService(InputMethodService);
            imm.HideSoftInputFromWindow(nameEditText.WindowToken, 0);
            ShortAlarm shortAlarm = new ShortAlarm();
            Room room = SQLiteDb.FindRoom(choosenRoom);
            string now = DateTime.Now.ToString("s");
            shortAlarm.Archived = false;
            shortAlarm.RoomId = room.Id;
            shortAlarm.Name = nameEditText.Text;
            shortAlarm.UserId = user.Id;
            shortAlarm.NotifyDate = Convert.ToDateTime(now);
            WebApiDataController.PostAlarm(this, shortAlarm);
            Task.Delay(3000);
            Finish();
        }
    }
}