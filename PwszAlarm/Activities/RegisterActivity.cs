using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PwszAlarm.Activities
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        private bool dataValid = false;
        private Button registerButton, registerBackButton;
        private EditText emailEditText, nickEditText, passwordEditText, confirmEditText, phoneEditText;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.Register);
            registerButton = FindViewById<Button>(Resource.Id.registerAccountButton);
            registerBackButton = FindViewById<Button>(Resource.Id.registerBackButton);
            emailEditText = FindViewById<EditText>(Resource.Id.registerEmailEditText);
            nickEditText = FindViewById<EditText>(Resource.Id.nickEditText);
            passwordEditText = FindViewById<EditText>(Resource.Id.registerPasswordEditText);
            confirmEditText = FindViewById<EditText>(Resource.Id.confirmEditText);
            phoneEditText = FindViewById<EditText>(Resource.Id.phoneEditText);
            string email = Intent.GetStringExtra("email");
            emailEditText.Text = email;
            registerButton.Click += RegisterButton_Click;
            registerBackButton.Click += RegisterBackButton_Click;

            emailEditText.TextChanged += EmailEditText_TextChanged;
            nickEditText.TextChanged += NickEditText_TextChanged;
            passwordEditText.TextChanged += PasswordEditText_TextChanged;
            confirmEditText.TextChanged += ConfirmEditText_TextChanged;
            phoneEditText.TextChanged += PhoneEditText_TextChanged;
        }

        private void PhoneEditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var phone = e.Text.ToString();
            if (phone.Length < 9 || phone.Length > 9)
            {
                phoneEditText.Error = "Podaj prawidłowy numer telefonu";
                dataValid = false;
            }
            else
            {
                dataValid = true;
            }
        }

        private void ConfirmEditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var password = e.Text.ToString(); 
            var passwordLenth = passwordEditText.Text.Length;
            if(password != passwordEditText.Text )
            {
                confirmEditText.Error = "Hasła różnią się";
                dataValid = false;
            }
            else
            {
                dataValid = true;
            }
        }

        private void PasswordEditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var password = e.Text.ToString();
            if(password.Length < 8)
            {
                passwordEditText.Error = "Hasło musi mieć min. 8 znaków";
                dataValid = false;
            }
            else
            {
                dataValid = true;
            }
        }

        private void NickEditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var nick = e.Text.ToString();
            if(nick == "")
            {
                nickEditText.Error = "To pole jest wymagane";
                dataValid = false;
            }
            else
            {
                dataValid = true;
            }
        }

        private void EmailEditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            EditText email = FindViewById<EditText>(Resource.Id.registerEmailEditText);
            var text = e.Text.ToString();
            var validEmail = Android.Util.Patterns.EmailAddress.Matcher(text).Matches();
            if(validEmail == false && text.Length >= 7)
            {
                email.Error = "Podaj poprawny adres e-mail";
                dataValid = false;
            }
            else
            {
                dataValid = true;
            }
        }

        private void RegisterBackButton_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            if(!dataValid)
            {
                TextView errorTextView = FindViewById<TextView>(Resource.Id.errorTextView);
                errorTextView.Visibility = ViewStates.Visible;
                return;
            }
            var registerSuceed = Register();
            Bundle extras = new Bundle();
            extras.PutString("email", emailEditText.Text);
            extras.PutBoolean("registered", true);
            FinishActivity(1);
            Intent intent = new Intent(this, typeof(SignInActivity));
            intent.PutExtras(extras);
            StartActivity(intent);
        }

        private bool Register()
        {
            var passwordHashed = HashLibrary.HashedPassword.New(passwordEditText.Text).Hash;
            return true;
        }

    }
}