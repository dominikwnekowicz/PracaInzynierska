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
using PwszAlarm.PwszAlarmDB;

namespace PwszAlarm.Activities
{
    [Activity(Label = "MainPageActivity")]
    public class MainPageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MainPage);
            var height = Resources.DisplayMetrics.HeightPixels / 9;
            var width = Resources.DisplayMetrics.WidthPixels;
            //LinearLayout leftLayout = FindViewById<LinearLayout>(Resource.Id.leftLinearLayout);
            SetButtonsSize(height * 2, width);
            Button planButton = FindViewById<Button>(Resource.Id.planButton);
            planButton.Click += (o, e) =>
            {
                var intent = new Intent(this, typeof(BuildingPlansActivity));
                StartActivity(intent);
            };
            Button reportButton = FindViewById<Button>(Resource.Id.reportButton);
            reportButton.Click += (o, e) =>
            {
                SQLiteDb.LoadRoomsToDB(this);
                var intent = new Intent(this, typeof(ReportEmergencyActivity));
                StartActivity(intent);
            };
            Button threatsButton = FindViewById<Button>(Resource.Id.threatsButton);
            threatsButton.Click += (o, e) =>
            {
                SQLiteDb.LoadAlarmsToDb(this);
                var intent = new Intent(this, typeof(AlarmsHistoryActivity));
                StartActivity(intent);
            };
        }
        private void SetButtonsSize(int height, int width)
        {
            //up
            Button policeButton = FindViewById<Button>(Resource.Id.policeButton);
            Button ambulanceButton = FindViewById<Button>(Resource.Id.ambulanceButton);
            Button fireButton = FindViewById<Button>(Resource.Id.fireButton);
            Button alarmButton = FindViewById<Button>(Resource.Id.alarmButton);
            policeButton.SetMinWidth(width / 4);
            fireButton.SetMinWidth(width / 4);
            ambulanceButton.SetMinWidth(width / 4);
            alarmButton.SetMinWidth(width / 4);

            //right side
            Button planButton = FindViewById<Button>(Resource.Id.planButton);
            Button threatsButton = FindViewById<Button>(Resource.Id.threatsButton);
            //Button reportButton = FindViewById<Button>(Resource.Id.reportButton);
            //planButton.SetMinHeight(height);
            planButton.SetMinWidth(width / 2);
            //threatsButton.SetMinHeight(height);
            threatsButton.SetMinWidth(width / 2);
            //reportButton.SetMinHeight(height*2);
            //reportButton.SetMaxHeight(height*2);



        }
    }
}