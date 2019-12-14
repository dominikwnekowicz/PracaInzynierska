using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Text.Format;
using Android.Content;
using Android.Preferences;

namespace PwszAlarm
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            var signedIn = prefs.GetBoolean("signedin", false);
            // Set our view from the "main" layout resource
            if (!signedIn)
            {
                var intent = new Intent(this, typeof(FirstRunActivity));
                StartActivityForResult(intent, 1);
            }
            SetContentView(Resource.Layout.activity_main);
            var height = Resources.DisplayMetrics.HeightPixels / 9;
            var width = Resources.DisplayMetrics.WidthPixels;
            //LinearLayout leftLayout = FindViewById<LinearLayout>(Resource.Id.leftLinearLayout);
            SetButtonsSize(height*2, width);
            
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void SetButtonsSize(int height, int width)
        {
            //up
            Button policeButton = FindViewById<Button>(Resource.Id.policeButton);
            Button ambulanceButton = FindViewById<Button>(Resource.Id.ambulanceButton);
            Button fireButton = FindViewById<Button>(Resource.Id.fireButton);
            Button alarmButton = FindViewById<Button>(Resource.Id.alarmButton);
            policeButton.SetMinWidth(width/4);
            fireButton.SetMinWidth(width/4);
            ambulanceButton.SetMinWidth(width/4);
            alarmButton.SetMinWidth(width/4);

            //right side
            Button planButton = FindViewById<Button>(Resource.Id.planButton);
            Button threatsButton = FindViewById<Button>(Resource.Id.threatsButton);
            //Button reportButton = FindViewById<Button>(Resource.Id.reportButton);
            //planButton.SetMinHeight(height);
            planButton.SetMinWidth(width/2);
            //threatsButton.SetMinHeight(height);
            threatsButton.SetMinWidth(width/2);
            //reportButton.SetMinHeight(height*2);
            //reportButton.SetMaxHeight(height*2);



        }
    }
}