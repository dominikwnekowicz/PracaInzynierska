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
using PwszAlarm.Model;
using Plugin.Connectivity;
using AlertDialog = Android.App.AlertDialog;

namespace PwszAlarm.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]

    public class MainActivity : AppCompatActivity
    {
        LoggedUser user;
        PermissionStatus status = PermissionStatus.Unknown;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoadingView);
            Permissions.CheckPermissions(this);
            do
            {
                PermissionsGranted();
                if(status == PermissionStatus.Granted)
                {
                    TurnOn();
                }
            }
            while (status != PermissionStatus.Granted);

        }
        private void TurnOn()
        {
            user = SQLiteDb.GetUser();
            if (user.Email == "failed")
            {
                var intent = new Intent(this, typeof(SignInActivity));
                StartActivityForResult(intent, 1);
                Finish();
            }
            else if (CrossConnectivity.Current.IsConnected)
            {
                if (user.RememberMe && DateTime.Now.Subtract(user.AuthorizationTime).TotalSeconds >= 86399)
                {
                    //Remember me, Authorization key not actve
                    var logged = WebApiDataController.LogIn(user).Result;
                    if (logged)
                    {
                        //logged in
                        var intent = new Intent(this, typeof(LoadDataActivity));
                        StartActivity(intent);
                        Finish();
                    }
                    else
                    {
                        //not logged in
                        AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                        AlertDialog alert = dialog.Create();
                        alert.SetTitle("Błąd");
                        alert.SetMessage("Logowanie nie było możliwe. Wprowadź dane logowania.");
                        alert.SetButton("OK", (c, ev) =>
                        {
                            alert.Hide();
                            var intent = new Intent(this, typeof(SignInActivity));
                            StartActivity(intent);
                            Finish();
                        });
                        alert.Show();
                    }

                }
                else if (!user.RememberMe && DateTime.Now.Subtract(user.AuthorizationTime).TotalSeconds >= 86399)
                {
                    //Dont remember me
                    var intent = new Intent(this, typeof(SignInActivity));
                    StartActivityForResult(intent, 1);
                    Finish();
                }
                else
                {   //Authorization key still active
                    var intent = new Intent(this, typeof(LoadDataActivity));
                    StartActivity(intent);
                    Finish();
                }
            }
            else
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Błąd");
                alert.SetMessage("Brak połączenia z internetem");
                alert.SetButton("SPRÓBUJ PONOWNIE", (c, ev) =>
                {
                    alert.Hide();
                    TurnOn();
                });
                alert.SetButton2("OK", (c, ev) =>
                {
                    alert.Hide();
                    Finish();
                });
                alert.Show();
            }
        }
        private async void PermissionsGranted()
        {
            var location = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationAlways);
            var storage = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if (location == PermissionStatus.Granted && storage == PermissionStatus.Granted) status = PermissionStatus.Granted;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            //Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}