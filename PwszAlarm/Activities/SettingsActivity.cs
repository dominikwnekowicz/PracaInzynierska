using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace PwszAlarm.Activities
{
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Settings);
            // Create your application here
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.settingsToolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Ustawienia";
        }
    }
}