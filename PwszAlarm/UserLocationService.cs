using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Location;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PwszAlarm.PwszAlarmDB;

namespace PwszAlarm
{
    [BroadcastReceiver]
    public class UserLocationService : BroadcastReceiver
    {
        public static string ACTION_PROCESS_LOCATION = "PwszAlarm.UPDATE_LOCATION";

        public override async void OnReceive(Context context, Intent intent)
        {
            if(intent != null)
            {
                string action = intent.Action;
                if(action.Equals(ACTION_PROCESS_LOCATION))
                {
                    LocationResult result = LocationResult.ExtractResult(intent);
                    if(result != null)
                    {
                        var location = result.LastLocation;
                        Location pwsz = new Location("PWSZ");
                        pwsz.Latitude = 49.609080;
                        pwsz.Longitude = 20.704225;
                        float[] results = new float[3];
                        Location.DistanceBetween(location.Latitude, location.Longitude, pwsz.Latitude, pwsz.Longitude, results);
                        var distance = results[0];
                        if(distance < 200)
                        {
                            await WebApiDataController.SetSendNotificationsAsync(true);
                        }
                        else
                        {
                            await WebApiDataController.SetSendNotificationsAsync(false);
                        }
                    }
                }
            }
        }
    }
}