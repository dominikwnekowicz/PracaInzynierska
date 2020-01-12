using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Text.Format;
using Android.Content;
using Android.Preferences;
using Android.Content.PM;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using PwszAlarm.PwszAlarmDB;
using System;
using Permission = Plugin.Permissions.Abstractions.Permission;
using System.Threading.Tasks;

namespace PwszAlarm.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]

    public class MainActivity : AppCompatActivity
    {
        PermissionStatus status = PermissionStatus.Unknown;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.LoadingView);
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            var signedIn = prefs.GetBoolean("signedin", false);
            Permissions.CheckPermissions(this);
            do
            {
                PermissionsGranted();
                if(status == PermissionStatus.Granted)
                {
                    if (!signedIn)
                    {
                        var intent = new Intent(this, typeof(SignInActivity));
                        StartActivityForResult(intent, 1);
                        Finish();
                    }
                    else
                    {
                        var intent = new Intent(this, typeof(LoadDataActivity));
                        StartActivity(intent);
                        Finish();
                    }
                }
                else
                {

                }
            }
            while (status != PermissionStatus.Granted);

        }
        private async void PermissionsGranted()
        {
            status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            //Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}