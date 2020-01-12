using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PwszAlarm.Activities
{
    [Activity(Label = "SignInActivity")]
    public class SignInActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SignIn);

            var registered = Intent.GetBooleanExtra("registered", false);
            Button registerButton = FindViewById<Button>(Resource.Id.registerButton);
            Button signinButton = FindViewById<Button>(Resource.Id.signinButton);
            EditText emailEditText = FindViewById<EditText>(Resource.Id.emailEditText);
            if(registered)
            {
                emailEditText.Text = Intent.GetStringExtra("email");
            }
            registerButton.Click += RegisterButton_Click;
            signinButton.Click += SigninButton_Click;

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutBoolean("registered", true);
            editor.Apply();
        }

        private void SigninButton_Click(object sender, EventArgs e)
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutBoolean("signedin", true);
            editor.Apply();
            var intent = new Intent(this, typeof(LoadDataActivity));
            StartActivity(intent);
            Finish();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {

            EditText emailEditText = FindViewById<EditText>(Resource.Id.emailEditText);
            var intent = new Intent(this, typeof(RegisterActivity));

            intent.PutExtra("email", emailEditText.Text);
            StartActivity(intent);
        }
    }
}