using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using PwszAlarm.Model;
using PwszAlarm.PwszAlarmDB;
using Refractored.Fab;

namespace PwszAlarm.Activities
{
    [Activity(Label = "AlarmsActivity")]
    public class AlarmsHistoryActivity : AppCompatActivity
    {
        private class AlarmsString
        {
            public int Id { get; set; }
            public string Text { get; set; }
        }
        List<Alarm> alarmsList;
        List<Alarm> alarms = new List<Alarm>();
        List<AlarmsString> alarmsStringsList = new List<AlarmsString>();
        List<Room> rooms;
        EditText alarmsSearchView;
        Button newAlarmsButton;
        Button archivedAlarmsButton;
        ListView alarmsListView;




        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AlarmsHistory);
            var width = Resources.DisplayMetrics.WidthPixels - (int)(20 * Resources.DisplayMetrics.Density);
            newAlarmsButton = FindViewById<Button>(Resource.Id.newAlarmsButton);
            newAlarmsButton.SetWidth(width / 2);
            newAlarmsButton.Click += NewAlarmsButton_Click;
            archivedAlarmsButton = FindViewById<Button>(Resource.Id.archivedAlarmsButton);
            archivedAlarmsButton.SetWidth(width / 2);
            archivedAlarmsButton.Click += ArchivedAlarmsButton_Click;

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.alarmsToolbar);
            alarmsSearchView = FindViewById<EditText>(Resource.Id.alarmsSearchEditText);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Alarmy";
            //Toolbar tabsToolbar = FindViewById<Toolbar>(Resource.Id.tabsToolbar);
            rooms = SQLiteDb.GetRooms(this).ToList();
            alarmsList = SQLiteDb.GetAlarms(this).GetAwaiter().GetResult().OrderByDescending(a => a.NotifyDate).ToList();

            alarmsSearchView.TextChanged += AlarmsSearchView_TextChanged; ;

            alarmsListView = FindViewById<ListView>(Resource.Id.alarmsListView);
            alarmsListView.ItemClick += AlarmsListView_ItemClick;

            await LoadList(false);

            var floatingButton = FindViewById<FloatingActionButton>(Resource.Id.alarmsHistoryFAB);

            floatingButton.Click += (o, e) =>
            {
                var intent = new Intent(this, typeof(ReportEmergencyActivity));
                StartActivity(intent);
                Finish();
            };

        }
        public void ClickItem(Alarm alarm)
        {
            var intent = new Intent(this, typeof(ChatActivity));
            intent.PutExtra("alarmId", alarm.Id);
            StartActivity(intent);
        }
        public void ClickItemButton(Alarm alarm)
        {
            var intent = new Intent(this, typeof(AlarmActivity));
            intent.PutExtra("alarmId", alarm.Id);
            StartActivity(intent);
        }
        private Task SetAdapter()
        {
            alarms = new List<Alarm>();
            foreach (var alarm in alarmsStringsList)
            {
                alarms.Add(alarmsList.FirstOrDefault(a => a.Id == alarm.Id));
            }
            var adapter = new AlarmsHistoryAdapter(this, alarms);
            alarmsListView = FindViewById<ListView>(Resource.Id.alarmsListView);
            alarmsListView.Adapter = adapter;
            return Task.CompletedTask;
        }
        private async Task LoadList(bool archived)
        {
            alarmsStringsList = new List<AlarmsString>();
            foreach (var alarm in alarmsList)
            {
                if (alarm.Archived == archived)
                {
                    var alarmString = new AlarmsString
                    {
                        Text = alarm.Name + " - " + rooms.FirstOrDefault(r => r.Id == alarm.RoomId).Name,
                        Id = alarm.Id
                    };
                    alarmsStringsList.Add(alarmString);
                }
            }
            await SetAdapter();
        }
        private async void ArchivedAlarmsButton_Click(object sender, EventArgs e)
        {
            archivedAlarmsButton.SetBackgroundResource(Resource.Drawable.ButtonBorder);
            newAlarmsButton.SetBackgroundColor(Android.Graphics.Color.White);
            await LoadList(true);
        }

        private async void NewAlarmsButton_Click(object sender, EventArgs e)
        {
            newAlarmsButton.SetBackgroundResource(Resource.Drawable.ButtonBorder);
            archivedAlarmsButton.SetBackgroundColor(Android.Graphics.Color.White);
            await LoadList(false);
        }

        protected override void OnResume()
        {
            alarmsSearchView.Text = "";
            base.OnResume();
        }

        private void AlarmsSearchView_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            alarms = new List<Alarm>();
            var alarmsTemp = alarmsStringsList.Where(a => a.Text.ToLower().Contains(e.Text.ToString().ToLower()));
            foreach (var alarm in alarmsTemp)
            {
                alarms.Add(alarmsList.FirstOrDefault(a => a.Id == alarm.Id));
            }
            var adapter = new AlarmsHistoryAdapter(this, alarms);
            alarmsListView = FindViewById<ListView>(Resource.Id.alarmsListView);
            alarmsListView.Adapter = adapter;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            //change main_compat_menu
            MenuInflater.Inflate(Resource.Menu.alarms_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            InputMethodManager inputMethodManager = GetSystemService(InputMethodService) as InputMethodManager;
            if (alarmsSearchView.Visibility != ViewStates.Visible)
            {
                alarmsSearchView.Visibility = ViewStates.Visible;
                if (alarmsSearchView.RequestFocus())
                {
                    inputMethodManager.ShowSoftInput(alarmsSearchView, ShowFlags.Forced);
                }
            }
            else
            {
                alarmsSearchView.Visibility = ViewStates.Gone;
                alarmsSearchView.Text = "";
                inputMethodManager.HideSoftInputFromWindow(alarmsSearchView.WindowToken, HideSoftInputFlags.None);
            }
            return base.OnOptionsItemSelected(item);
        }
        private void AlarmsListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var alarm = alarmsList.FirstOrDefault(a => a.Id == alarms.ElementAt(e.Position).Id);
            var intent = new Intent(this, typeof(ChatActivity));
            intent.PutExtra("alarmId", alarm.Id);
            StartActivity(intent);
            Finish();
        }
    }
}