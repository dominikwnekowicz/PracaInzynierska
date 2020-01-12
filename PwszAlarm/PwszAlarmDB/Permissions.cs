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
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                {
                    SQLiteDb.ShowAlert(activity, "Uprawnienia", "Potrzebujemy uprawnień do dostępu do pamięci.");
                }
                var results = CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
            }
        }
    }
}