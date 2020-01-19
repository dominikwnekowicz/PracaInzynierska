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
using Newtonsoft.Json;

namespace PwszAlarm.Model
{
    public class RegisterUser
    {
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "confirmPassword")]
        public string ConfirmPassword { get; set; }
    }
    public class LoggedUser
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }
        public string Password { get; set; }

        [JsonProperty(PropertyName = "fullName")]
        public string FullName { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "emailConfirmed")]
        public bool EmailConfirmed { get; set; }

        [JsonProperty(PropertyName = "joinDate")]
        public DateTime JoinDate { get; set; }
        public string Authorization { get; set; }
        public DateTime AuthorizationTime { get; set; }
        public bool RememberMe { get; set; }
    }
    public class AuthToken
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int AuthDT { get; set; }
    }
}