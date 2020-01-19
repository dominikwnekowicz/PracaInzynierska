using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Android.Accounts;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using PwszAlarm.Model;
using SQLite;

namespace PwszAlarm.PwszAlarmDB
{
    public class WebApiDataController
    {
        public static void GetRoomsFromApi(SQLiteConnection db)
        {
            IEnumerable<Room> rooms;
            LoggedUser loggedUser = SQLiteDb.GetUser();
            if (loggedUser.Email != "failed")
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loggedUser.Authorization);
                var url = "http://192.168.1.10/PwszAlarmAPI/api/rooms";
                //GET/api/rooms
                var content = httpClient.GetStringAsync(url).Result;
                rooms = JsonConvert.DeserializeObject<List<Room>>(content);
                if (rooms.Any())
                {
                    var cmd = db.CreateCommand("DELETE FROM Room");
                    cmd.ExecuteQuery<Room>();
                    db.InsertAll(rooms);
                }
            }
        }
        public static void GetAlarmsFromApi(SQLiteConnection db)
        {
            IEnumerable<Alarm> alarms;
            LoggedUser loggedUser = SQLiteDb.GetUser();
            if (loggedUser.Email != "failed")
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loggedUser.Authorization);
                var url = "http://192.168.1.10/PwszAlarmAPI/api/alarms";
                //GET/api/alarms
                var content = httpClient.GetStringAsync(url).Result;
                alarms = JsonConvert.DeserializeObject<List<Alarm>>(content);
                if (alarms.Any())
                {
                    var cmd = db.CreateCommand("DELETE FROM Alarm");
                    cmd.ExecuteNonQuery();
                    db.InsertAll(alarms);
                }
            }
        }
        public static async Task PostAlarm(Activity activity, ShortAlarm shortAlarm)
        {
            LoggedUser loggedUser = SQLiteDb.GetUser();
            if (loggedUser.Email != "failed")
            {
                var httpClient = new HttpClient();
                var url = "http://192.168.1.10/PwszAlarmAPI/api/alarms";
                //POST /api/alarms
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loggedUser.Authorization);
                var alarmJson = JsonConvert.SerializeObject(shortAlarm);
                var httpContent = new StringContent(alarmJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await httpClient.PostAsync(url, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    Toast.MakeText(activity, "Zgłoszenie wysłane", ToastLength.Short).Show();
                    SQLiteDb.LoadAlarmsToDb(activity);
                }
            }
        }
        public static async Task<bool> RegisterUser(Activity activity, RegisterUser user)
        {
            using (var httpClient = new HttpClient())
            {
                var url = "http://192.168.1.10/PwszAlarmAPI/api/accounts/create";
                var userJson = JsonConvert.SerializeObject(user);
                var httpContent = new StringContent(userJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await httpClient.PostAsync(url, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    Toast.MakeText(activity, "Zarejestrowano pomyślnie", ToastLength.Short).Show();
                    return true;
                }
                return false;
            }
        }
        public static async Task<bool> LogIn(Activity activity, LoggedUser user)
        {
            AuthToken authToken = Authorize(user);
            if (authToken.AuthDT != 0)
            {
                LoggedUser loggedUser = SQLiteDb.GetUser();
                if(loggedUser.Email != user.Email)
                {
                    var httpClient = new HttpClient();
                    var url = "http://192.168.1.10/PwszAlarmAPI/api/accounts/user?username=" + user.UserName;
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken.AccessToken);
                    var content = await httpClient.GetStringAsync(url);
                    loggedUser = JsonConvert.DeserializeObject<LoggedUser>(content);
                    if (loggedUser.Email != user.Email)
                    {
                        return false;
                    }
                }
                user.AuthorizationTime = DateTime.Now;
                user.Authorization = authToken.AccessToken;
                loggedUser.RememberMe = user.RememberMe;
                if (user.RememberMe) loggedUser.Password = user.Password;
                SQLiteDb.UpdateAccountData(loggedUser);
                return true;
            }
            else
            {
                return false;
            }
        }
        private static AuthToken Authorize(LoggedUser user)
        {
            AuthToken authToken = new AuthToken();
            var httpClient = new HttpClient();
            var url = "http://192.168.1.10/PwszAlarmAPI/oauth/token";
            string body = "username=" + user.UserName + "&password=" + user.Password + "&grant_type=password";
            var httpContent = new StringContent(body);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpContent.Headers.ContentType.CharSet = "utf8";
            var response = httpClient.PostAsync(url, httpContent).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                authToken = JsonConvert.DeserializeObject<AuthToken>(responseContent);
                return authToken;
            }
            else
            {
                authToken.AccessToken = "failed";
                authToken.AuthDT = 0;
                return authToken;
            }
        }
    }
}