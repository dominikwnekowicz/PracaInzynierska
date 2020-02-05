using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Location;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using PwszAlarm.Model;
using PwszAlarm.PwszAlarmDB;
using Refractored.Fab;

namespace PwszAlarm.Activities
{
    [Activity(Label = "MainPageActivity")]
    public class MainPageActivity : AppCompatActivity
    {
        LocationRequest locationRequest;
        FusedLocationProviderClient fusedLocationProviderClient;

        static MainPageActivity Instance;

        public static MainPageActivity GetInstance()
        {
            return Instance;
        }
        

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MainPage);
            Instance = this;
            var width = Convert.ToInt32(Resources.DisplayMetrics.WidthPixels * 0.96);

            var alarms = await SQLiteDb.GetAlarms(this);
            Alarm alarm = new Alarm();
            if (alarms.Count() > 0)
            {
                alarm = alarms.Last();
            }
            if (alarm != null)
            {
                var floatingButton = FindViewById<FloatingActionButton>(Resource.Id.mainPageFloatingActionButton);
                this.RunOnUiThread(() =>
                {
                    if (DateTime.Now.Subtract(alarm.NotifyDate).TotalHours > 24) floatingButton.Visibility = ViewStates.Gone;
                    else floatingButton.Visibility = ViewStates.Visible;
                });

                floatingButton.Click += (o, e) =>
                {
                    var intent = new Intent(this, typeof(AlarmActivity));
                    intent.PutExtra("alarmId", alarm.Id);
                    StartActivity(intent);
                };
            }

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            var notificationsAlways = prefs.GetBoolean("settingsNotifications", true);

            if(notificationsAlways)
            {
                await UpdateLocationAsync(false);
                await WebApiDataController.SetSendNotificationsAsync(true);
            }
            else
            {
                await UpdateLocationAsync(true);
            }

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
                Finish();
            };
        }

        private async System.Threading.Tasks.Task UpdateLocationAsync(bool turnOn)
        {
            BuildLocationRequest();
            fusedLocationProviderClient = LocationServices.GetFusedLocationProviderClient(this);
            if (await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location) != PermissionStatus.Granted) return;
            if(turnOn) fusedLocationProviderClient.RequestLocationUpdates(locationRequest, GetPendingIntent());
            else fusedLocationProviderClient.RemoveLocationUpdates(GetPendingIntent());
        }
        private PendingIntent GetPendingIntent()
        {
            Intent intent = new Intent(this, typeof(UserLocationService));
            intent.SetAction(UserLocationService.ACTION_PROCESS_LOCATION);
            return PendingIntent.GetBroadcast(this, 0, intent, PendingIntentFlags.UpdateCurrent);
        }
        private void BuildLocationRequest()
        {
            locationRequest = new LocationRequest();
            locationRequest.SetPriority(LocationRequest.PriorityHighAccuracy);
            locationRequest.SetInterval(5000);
            locationRequest.SetFastestInterval(3000);
            locationRequest.SetSmallestDisplacement(10f);
        }
    }
}