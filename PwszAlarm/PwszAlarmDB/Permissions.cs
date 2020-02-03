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
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace PwszAlarm.PwszAlarmDB
{
    public class Permissions : Application
    {
        public static async void CheckPermissions(Activity activity)
        {
            var storage = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            var location = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationAlways);
            if (storage != PermissionStatus.Granted || location != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                {
                    SQLiteDb.ShowAlert(activity, "Uprawnienia", "Potrzebujemy uprawnień do pamięci.");
                }
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationAlways))
                {
                    SQLiteDb.ShowAlert(activity, "Uprawnienia", "Potrzebujemy uprawnień do lokalizacji.");
                }
                Permission[] permissions =
                {
                    Permission.Storage,
                    Permission.LocationAlways
                };
                var results = await CrossPermissions.Current.RequestPermissionsAsync(permissions);
            }
        }
    }
}