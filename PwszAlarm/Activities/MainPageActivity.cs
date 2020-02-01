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
            var width = Convert.ToInt32(Resources.DisplayMetrics.WidthPixels * 0.96);
            //LinearLayout leftLayout = FindViewById<LinearLayout>(Resource.Id.leftLinearLayout);
            
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
                SQLiteDb.LoadAlarmsToDb(this).GetAwaiter();
                var intent = new Intent(this, typeof(AlarmsHistoryActivity));
                StartActivity(intent);
            };
            Button settingsButton = FindViewById<Button>(Resource.Id.settingsButton);
            settingsButton.Click += (o, e) =>
            {
                var intent = new Intent(this, typeof(SettingsActivity));
                StartActivity(intent);
            };
        }
    }
}