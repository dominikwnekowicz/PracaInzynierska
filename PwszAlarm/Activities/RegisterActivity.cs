using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PwszAlarm.Model;
using PwszAlarm.PwszAlarmDB;

namespace PwszAlarm.Activities
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        private bool dataValid = false;
        private Button registerButton, registerBackButton;
        private EditText emailEditText, passwordEditText, confirmEditText, firstNameEditText, lastNameEditText;
        RegisterUser registerUser = new RegisterUser();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.Register);
            registerButton = FindViewById<Button>(Resource.Id.registerAccountButton);
            registerBackButton = FindViewById<Button>(Resource.Id.registerBackButton);
            emailEditText = FindViewById<EditText>(Resource.Id.registerEmailEditText);
            passwordEditText = FindViewById<EditText>(Resource.Id.registerPasswordEditText);
            confirmEditText = FindViewById<EditText>(Resource.Id.confirmEditText);
            firstNameEditText = FindViewById<EditText>(Resource.Id.firstNameEditText);
            lastNameEditText = FindViewById<EditText>(Resource.Id.lastNameEditText);
            string email = Intent.GetStringExtra("email");
            emailEditText.Text = email;
            registerButton.Click += RegisterButton_ClickAsync;
            registerBackButton.Click += RegisterBackButton_Click;

            emailEditText.TextChanged += EmailEditText_TextChanged;
            passwordEditText.TextChanged += PasswordEditText_TextChanged;
            confirmEditText.TextChanged += ConfirmEditText_TextChanged;
            firstNameEditText.TextChanged += FirstNameEditText_TextChanged;
            lastNameEditText.TextChanged += LastNameEditText_TextChanged;

            confirmEditText.EditorAction += (sender, e) =>
            {
                if (e.ActionId == Android.Views.InputMethods.ImeAction.Done)
                {
                    registerButton.PerformClick();
                }
                else
                {
                    e.Handled = false;
                }
            };
        }

        private void LastNameEditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var text = e.Text.ToString();
            if (text.Length < 3 && text.Length != 0)
            {
                lastNameEditText.Error = "Pole jest wymagane";
                dataValid = false;
            }
            else
            {
                dataValid = true;
                registerUser.LastName = text;
            }
        }

        private void FirstNameEditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var text = e.Text.ToString();
            if ( text.Length < 3 && text.Length != 0 )
            {
                firstNameEditText.Error = "Pole jest wymagane";
                dataValid = false;
            }
            else
            {
                dataValid = true;
                registerUser.FirstName = text;
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
                registerUser.ConfirmPassword = password;
            }
        }

        private void PasswordEditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var password = e.Text.ToString();
            if(password.Length < 8 && password.Length != 0 || !password.ToCharArray().Any(char.IsUpper) || !password.ToCharArray().Any(char.IsDigit) || !password.ToCharArray().Any(char.IsPunctuation))
            {
                passwordEditText.Error = "Hasło musi mieć min. 8 znaków, zawierać dużą literę, cyfrę i symbol";
                dataValid = false;
            }
            else
            {
                dataValid = true;
                registerUser.Password = password;
            }
        }

        private void EmailEditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var text = e.Text.ToString();
            var validEmail = Android.Util.Patterns.EmailAddress.Matcher(text).Matches();
            if(!validEmail && text.Length >= 7)
            {
                emailEditText.Error = "Podaj poprawny adres e-mail";
                dataValid = false;
            }
            else
            {
                dataValid = true;
                registerUser.Email = text;
            }
        }

        private void RegisterBackButton_Click(object sender, EventArgs e)
        {
            Finish();
        }
        private bool EmptyField()
        {
            if (!confirmEditText.Text.Any() || !emailEditText.Text.Any() || !firstNameEditText.Text.Any() || !lastNameEditText.Text.Any() || !passwordEditText.Text.Any()) return true;
            return false;
            
        }
        private async void RegisterButton_ClickAsync(object sender, EventArgs e)
        {
            TextView errorTextView = FindViewById<TextView>(Resource.Id.errorTextView);
            if (!dataValid || EmptyField())
            {
                errorTextView.Text = "Wypełnij dane poprawnie...";
                errorTextView.Visibility = ViewStates.Visible;
                return;
            }
            registerUser.UserName = registerUser.Email.Split('@').ElementAt(0);
            var registerSuceed = await WebApiDataController.RegisterUser(this, registerUser);
            if(!registerSuceed)
            {
                errorTextView.Text = "Błąd, spróbuj ponownie...";
                errorTextView.Visibility = ViewStates.Visible;
                return;
            }
            SQLiteDb.ShowAlert(this, "Ważne!", "Aktywuj konto linkiem wysłanym w wiadomości e-mail. Link wygasa za 6 godzin.");
            await Task.Delay(3000);
            errorTextView.Visibility = ViewStates.Gone;
            Bundle extras = new Bundle();
            extras.PutString("email", emailEditText.Text);
            extras.PutBoolean("registered", true);
            FinishActivity(1);
            Intent intent = new Intent(this, typeof(SignInActivity));
            intent.PutExtras(extras);
            StartActivity(intent);
        }
    }
}