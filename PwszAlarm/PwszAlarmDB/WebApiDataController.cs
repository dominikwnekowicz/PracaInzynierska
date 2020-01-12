using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
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
        public async static void GetRoomsFromApi(SQLiteConnection db)
        {
            IEnumerable<Room> rooms;
            using (var httpClient = new HttpClient())
            {
                var url = "http://192.168.1.13/PwszAlarmAPI/api/rooms";

                //GET/api/rooms
                var content = await httpClient.GetStringAsync(url);
                rooms = JsonConvert.DeserializeObject<List<Room>>(content);
                if (rooms.Any())
                {
                    var cmd = db.CreateCommand("DELETE FROM Room");
                    cmd.ExecuteQuery<Room>();
                    db.InsertAll(rooms);
                }
            }
        }
        public async static void GetAlarmsFromApi(SQLiteConnection db)
        {
            IEnumerable<Alarm> alarms;
            using (var httpClient = new HttpClient())
            {
                var url = "http://192.168.1.13/PwszAlarmAPI/api/alarms";

                //GET/api/alarms
                var content = await httpClient.GetStringAsync(url);
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
            using (var httpClient = new HttpClient())
            {
                var url = "http://192.168.1.13/PwszAlarmAPI/api/alarms";
                //POST /api/alarms
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
    }
}