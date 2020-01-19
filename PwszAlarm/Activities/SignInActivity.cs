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
using PwszAlarm.Model;
using PwszAlarm.PwszAlarmDB;

namespace PwszAlarm.Activities
{
    [Activity(Label = "SignInActivity")]
    public class SignInActivity : Activity
    {
        private bool dataValid = false;
        EditText emailEditText, passwordEditText;
        CheckBox rememberMe;
        LoggedUser loggedUser = new LoggedUser();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SignIn);

            var registered = Intent.GetBooleanExtra("registered", false);
            Button registerButton = FindViewById<Button>(Resource.Id.registerButton);
            Button signinButton = FindViewById<Button>(Resource.Id.signinButton);
            emailEditText = FindViewById<EditText>(Resource.Id.emailEditText);
            passwordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);
            rememberMe = FindViewById<CheckBox>(Resource.Id.rememberCheckBox);
            if(registered)
            {
                emailEditText.Text = Intent.GetStringExtra("email");
            }
            registerButton.Click += RegisterButton_Click;
            signinButton.Click += SigninButton_Click;
            emailEditText.TextChanged += EmailEditText_TextChanged;
            passwordEditText.TextChanged += PasswordEditText_TextChanged;

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutBoolean("registered", true);
            editor.Apply();
        }

        private void PasswordEditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var password = e.Text.ToString();
            if (password.Length < 8 && password.Length != 0 || !password.ToCharArray().Any(char.IsUpper) || !password.ToCharArray().Any(char.IsDigit) || !password.ToCharArray().Any(char.IsPunctuation))
            {
                passwordEditText.Error = "Hasło musi mieć min. 8 znaków, zawierać dużą literę, cyfrę i symbol";
                dataValid = false;
            }
            else
            {
                dataValid = true;
                loggedUser.Password = password;
            }
        }

        private void EmailEditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var text = e.Text.ToString();
            var validEmail = Android.Util.Patterns.EmailAddress.Matcher(text).Matches();
            if (!validEmail && text.Length >= 7)
            {
                emailEditText.Error = "Podaj poprawny adres e-mail";
                dataValid = false;
            }
            else
            {
                dataValid = true;
                loggedUser.Email = text;
                loggedUser.UserName = text.Split('@').ElementAt(0);
               
            }
        }

        private async void SigninButton_Click(object sender, EventArgs e)
        {
            bool loggedIn;
            if (!dataValid )
            {
                SQLiteDb.ShowAlert(this, "Błąd", "Nie udało się zalogować, spróbuj ponownie później.");
                return;
            }
            else
            {
                loggedUser.RememberMe = rememberMe.Selected;
                loggedIn = await WebApiDataController.LogIn(this, loggedUser);
                
            }
            if(loggedIn)
            {
                SQLiteDb.UpdateAccountData(loggedUser);
                var intent = new Intent(this, typeof(LoadDataActivity));
                StartActivity(intent);
                Finish();
            }
            else
            {
                SQLiteDb.ShowAlert(this, "Błąd", "Nie udało się zalogować, spróbuj ponownie później.");
            }
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