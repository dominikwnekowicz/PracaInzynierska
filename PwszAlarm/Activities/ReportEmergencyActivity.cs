using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using PwszAlarm.Adapters;
using PwszAlarm.Model;
using PwszAlarm.PwszAlarmDB;

namespace PwszAlarm.Activities
{
    [Activity(Label = "ReportEmergencyActivity")]
    public class ReportEmergencyActivity : AppCompatActivity
    {
        RoomsListAdapter adapter;
        ExpandableListView expandableListView;
        List<Room> rooms = new List<Room>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RoomsList);

            rooms = SQLiteDb.GetRooms(this).ToList();

            expandableListView = FindViewById<ExpandableListView>(Resource.Id.roomsExpandableListView);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.roomsListToolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Zgłoś zagrożenie";

            adapter = new RoomsListAdapter(this, rooms);
            expandableListView.SetAdapter(adapter);

            expandableListView.ChildClick += (o, e) =>
            {
                var roomName = adapter.GetChild(e.GroupPosition, e.ChildPosition).ToString();
                var floorName = adapter.GetGroup(e.GroupPosition).ToString();
                var intent = new Intent(this, typeof(WriteReportDataActivity));
                Bundle extras = new Bundle();
                extras.PutString("choosenFloor", floorName);
                extras.PutString("choosenRoom", roomName);
                intent.PutExtras(extras);
                StartActivity(intent);
                Finish();
            };
        }
    }
}