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
using PwszAlarm.PwszAlarmDB;

namespace PwszAlarm.Activities
{
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : AppCompatActivity
    {
        ISharedPreferences prefs;
        ISharedPreferencesEditor editor;
        public override void OnBackPressed()
        {
            var intent = new Intent(this, typeof(MainPageActivity));
            StartActivity(intent);
            Finish();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Settings);
            // Create your application here
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.settingsToolbar);
            SetSupportActionBar(toolbar);
            prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            editor = prefs.Edit();
            SupportActionBar.Title = "Ustawienia";
            Switch gpsSwitch = FindViewById<Switch>(Resource.Id.gpsSwitch);
            gpsSwitch.Checked = prefs.GetBoolean("settingsNotifications", true);
            ChangeText(gpsSwitch);

            Button logout = FindViewById<Button>(Resource.Id.logOutButton);
            gpsSwitch.CheckedChange += (o, e) =>
            {
                editor.PutBoolean("settingsNotifications", e.IsChecked);
                editor.Apply();
                ChangeText(gpsSwitch);
            };

            logout.Click += async (o, e) =>
            {
                await SQLiteDb.LogOut();
                FinishActivity(0);
                var intent = new Intent(this, typeof(SignInActivity));
                StartActivity(intent);
                Finish();
            };
        }
        private void ChangeText(Switch gpsSwitch)
        {
            if (gpsSwitch.Checked) gpsSwitch.Text = "Powiadomienia: Zawsze";
            else gpsSwitch.Text = "Powiadomienia: W budynku";
        }

    }
}