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
using Android.Widget;
using PwszAlarm.PwszAlarmDB;

namespace PwszAlarm.Activities
{
    [Activity(Label = "PwszAlarm")]
    public class LoadDataActivity : Activity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoadingView);
            SQLiteDb.LoadRoomsToDB(this);
            await SQLiteDb.LoadAlarmsToDb(this);
            await Task.Delay(1000);
            var intent = new Intent(this, typeof(MainPageActivity));
            StartActivity(intent);
            Finish();
            
            // Create your application here
        }
    }
}