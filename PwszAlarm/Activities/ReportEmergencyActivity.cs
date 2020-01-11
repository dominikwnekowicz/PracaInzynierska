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

namespace PwszAlarm
{
    [Activity(Label = "ReportEmergencyActivity")]
    public class ReportEmergencyActivity : Activity
    {
        private List<string> roomsStringList = new List<string>();
        private List<string> floorsStringList = new List<string>();
        private IEnumerable<Room> roomsList;
        private ListView reportListView;
        private int page = 1;
        private string choosenFloor;

        public override void OnBackPressed()
        {
            if (page == 2)
            {
                ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, floorsStringList);
                reportListView.Adapter = adapter;
                roomsStringList.Clear();
                page--;
            }
            else
            {
                base.OnBackPressed();
            }

        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ReportEmergency);
            reportListView = FindViewById<ListView>(Resource.Id.reportListView);
            roomsList = SQLiteDb.GetRooms(this);
            if (!roomsList.Any())
            {
                floorsStringList.Add("Brak danych");
            }
            else
            {;
                floorsStringList.Add("Parter");
                floorsStringList.Add("Pierwsze piętro");
                floorsStringList.Add("Drugie piętro");
            }
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, floorsStringList);
            reportListView.Adapter = adapter;
            reportListView.ItemClick += ListView_ItemClick;
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var selectedItem = e.Position;
            if (page == 1)
            {
                var floor = floorsStringList.ElementAt(selectedItem);
                foreach (Room r in roomsList.ToList())
                {
                    if (r.Floor == floor)
                    {
                        roomsStringList.Add("Sala " + r.Name);
                    }
                }
                ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, roomsStringList);
                reportListView.Adapter = adapter;
                page++;
                if(selectedItem == 0) choosenFloor = floorsStringList.ElementAt(selectedItem);
                else choosenFloor = floorsStringList.ElementAt(selectedItem).Split(" ").ElementAt(0);
            }
            else
            {
                var choosenRoom = roomsStringList.ElementAt(selectedItem).Split(" ").ElementAt(1);
                var intent = new Intent(this, typeof(WriteReportDataActivity));
                Bundle extras = new Bundle();
                extras.PutString("choosenFloor", choosenFloor);
                extras.PutString("choosenRoom", choosenRoom);
                intent.PutExtras(extras);
                StartActivity(intent);
            }
        }
    }
}