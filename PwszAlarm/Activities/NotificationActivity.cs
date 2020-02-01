using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Iid;
using Firebase.Messaging;
using PwszAlarm.PwszAlarmDB;

namespace PwszAlarm.Activities
{
    [Activity(Label = "NotificationActivity")]
    public class NotificationActivity : Activity
    {
        internal const string CHANNEL_ID = "notification_channel";
        internal const int NOTIFICATION_ID = 100;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoadingView);
            CreateNotificationChannel();
            IsPlayServicesAvailable();


            var token = FirebaseInstanceId.Instance.Token;
            WebApiDataController.PostNotifyToken(token);
            FirebaseMessaging.Instance.SubscribeToTopic("news");
            var intent = new Intent(this, typeof(LoadDataActivity));
            StartActivity(intent);
            Finish();
        }

        public bool IsPlayServicesAvailable()
        {
            var resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (!GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    Finish();
                }

                return false;
            }
            return true;
        }


        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification 
                // channel on older versions of Android.
                return;
            }

            var channel = new NotificationChannel(CHANNEL_ID, "FCM Notifications", NotificationImportance.Default)
            {
                Description = "Firebase Cloud Messages Alarms Channel"
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }
}
