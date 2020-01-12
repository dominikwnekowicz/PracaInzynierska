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
using PwszAlarm.Model;
using PwszAlarm.PwszAlarmDB;

namespace PwszAlarm.Activities
{
    [Activity(Label = "BuildingPlansActivity")]
    public class BuildingPlansActivity : Activity
    {
        private List<string> rooms;
        private ListView listView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BuildingPlans);
            listView = FindViewById<ListView>(Resource.Id.listView);
            var roomsList = SQLiteDb.GetRooms(this);
            rooms = new List<string>();
            if (!roomsList.Any())
            {
                rooms.Add("Brak danych");
            }
            else
            {
                foreach (Room r in roomsList)
                {
                    rooms.Add("Sala " + r.Name);
                }
            }
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, rooms);
            listView.Adapter = adapter;
            // Create your application here
        }
    }
}