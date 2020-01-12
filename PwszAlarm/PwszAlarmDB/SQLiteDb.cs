using System;
using System.Collections.Generic;
using System.IO;
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
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using PwszAlarm.Model;
using SQLite;
using Xamarin.Essentials;

namespace PwszAlarm.PwszAlarmDB
{
    public class SQLiteDb : Application
    {
        static IEnumerable<Room> roomsList;
        static IEnumerable<Alarm> alarmsList;
        private static bool TableExist(SQLiteConnection db, string tableName)
        {
            try
            {
                if (db.GetTableInfo(tableName).Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private static async void CheckPermissions(Activity activity)
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                {
                    ShowAlert(activity, "Uprawnienia", "Potrzebujemy uprawnień do dostępu do pamięci.");
                }
                var results = CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
            }
        }
        public static async void LoadAlarmsToDb(Activity activity)
        {
            CheckPermissions(activity);
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if (status == PermissionStatus.Granted)
            {
                string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
                string path = Path.Combine(documentsPath, "PwszAlarm.db3");
                const SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create;
                SQLiteConnection db = new SQLiteConnection(path, Flags);
                var result = TableExist(db, "Alarm");
                if (!result) db.CreateTable<Alarm>();
                alarmsList = db.Table<Alarm>().ToList();
                var connection = Connectivity.NetworkAccess;
                if (connection == NetworkAccess.Internet)
                {
                    WebApiDataController.GetAlarmsFromApi(db);
                    alarmsList = db.Table<Alarm>().ToList();
                }
                else if (alarmsList.Any())
                {
                    return;
                }
                else
                {
                    ShowAlert(activity, "Błąd", "Nie udało się załadować bazy danych. Brak połączenia z internetem.");
                }
            }
            else
            {
                ShowAlert(activity, "Brak uprawnień", "Brak dostępu do pamięci urządzenia, nie można zapisać pliku bazy danych.");
            }
        }
        public static async void LoadRoomsToDB(Activity activity)
        {
            CheckPermissions(activity);
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if(status == PermissionStatus.Granted)
            {
                string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
                string path = Path.Combine(documentsPath, "PwszAlarm.db3");
                const SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create;
                SQLiteConnection db = new SQLiteConnection(path, Flags);
                var result = TableExist(db, "Room");
                if (!result) db.CreateTable<Room>();
                roomsList = db.Table<Room>().ToList();
                var connection = Connectivity.NetworkAccess;
                if (connection == NetworkAccess.Internet)
                {
                    WebApiDataController.GetRoomsFromApi(db);
                    roomsList = db.Table<Room>().ToList();
                }
                else if (roomsList.Any())
                {
                    return;
                }
                else
                {
                    ShowAlert(activity, "Błąd", "Nie udało się załadować bazy danych. Brak połączenia z internetem.");
                }
            }
            else
            {
                ShowAlert(activity, "Brak uprawnień", "Brak dostępu do pamięci urządzenia, nie można zapisać pliku bazy danych.");
            }
        }
        public static IEnumerable<Room> GetRooms(Activity activity)
        {
            if (!roomsList.Any()) LoadRoomsToDB(activity);
            return roomsList;
        }
        public static IEnumerable<Alarm> GetAlarms(Activity activity)
        {
            if (!alarmsList.Any()) LoadAlarmsToDb(activity);
            return alarmsList;
        }
        public static void ShowAlert(Activity activity, string title, string message)
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(activity);
            AlertDialog alert = dialog.Create();
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetButton("OK", (c, ev) =>
            {
                alert.Hide();
            });
            alert.Show();
        }
        public static Room FindRoom(string roomName)
        {
            Room room = roomsList.FirstOrDefault(x => x.Name == roomName);
            return room;
        }
    }
}