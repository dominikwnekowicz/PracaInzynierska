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
using Microsoft.AspNet.SignalR.Client;
using PwszAlarm.Adapters;
using PwszAlarm.Model;
using PwszAlarm.PwszAlarmDB;

namespace PwszAlarm.Activities
{
    [Activity(Label = "ChatActivity", WindowSoftInputMode = SoftInput.AdjustResize)]
    public class ChatActivity : AppCompatActivity
    {
        Alarm alarm;
        ListView messagesListView;
        List<Messages> messagesList;
        EditText newMessageEditText;
        LoggedUser loggedUser;
        int width;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            width = Resources.DisplayMetrics.WidthPixels;
            var alarmId = Intent.GetIntExtra("alarmId", 1);
            alarm = SQLiteDb.GetAlarms(this).GetAwaiter().GetResult().FirstOrDefault(a => a.Id == alarmId);
            var room = SQLiteDb.GetRooms(this).FirstOrDefault(r => r.Id == alarm.Id);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ChatLayout);
            messagesListView = FindViewById<ListView>(Resource.Id.messagesListView);
            var newMessageLinearLayout = FindViewById<LinearLayout>(Resource.Id.newMessageLinearLayout);
            if (alarm.Archived == true) newMessageLinearLayout.Visibility = ViewStates.Gone;

            loggedUser = SQLiteDb.GetUser();
            LoadMessages(alarmId);
            newMessageEditText = FindViewById<EditText>(Resource.Id.newMessageEditText);
            newMessageEditText.SetWidth(Convert.ToInt32(width * 0.85));
            newMessageEditText.TextChanged += NewMessageEditText_TextChanged;

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            string title = alarm.Name;
            if(title.Length>17) title = alarm.Name.Substring(0, 17) + "...";
            SupportActionBar.Title = title;
            // Create your application here

            if (loggedUser.Email != "failed")
            {
                var userName = loggedUser.UserName;
                var hubConnection = new HubConnection("https://pwszalarmwebapi.azurewebsites.net");
                var messagesHubProxy = hubConnection.CreateHubProxy("MessagesHub");

                messagesHubProxy.On<int, int, string, string, DateTime>("sendMessageToClients", (id, alarmId, userName, message, messageTime) =>
                {
                    if (messagesList.FirstOrDefault().UserName == "failed" && messagesList.FirstOrDefault().Message == "failed") messagesList.Clear();
                        this.RunOnUiThread(() =>
                    {
                        var messageObj = new Messages
                        {
                            Id = id,
                            AlarmId = alarmId,
                            UserName = userName,
                            Message = message,
                            MessageTime = messageTime
                        };
                        messagesList.Add(messageObj);
                        DisplayMessages();
                       
                    });
                });
                try
                {
                    await hubConnection.Start();
                }
                catch (Exception ex)
                {
                    SQLiteDb.ShowAlert(this, "Błąd", ex.Message);
                }

                //Sending message
                var sendImageButton = FindViewById<ImageButton>(Resource.Id.sendMessageImageButton);
                sendImageButton.Click += async (o, e) =>
                {
                    InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
                    var message = newMessageEditText.Text;
                    if(!string.IsNullOrEmpty(message) ) await messagesHubProxy.Invoke("SendMessage", new object[] { alarmId, userName, message });
                    message = null;
                    newMessageEditText.Text = message;
                };
                newMessageEditText.EditorAction += (sender, e) =>
            {
                if (e.ActionId == ImeAction.Done)
                {
                    sendImageButton.PerformClick();
                }
                else
                {
                    e.Handled = false;
                }
            };
            }
            


        }

        private void NewMessageEditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (messagesList.FirstOrDefault().UserName == "failed" && messagesList.FirstOrDefault().Message == "failed") return;
            messagesListView.DeferNotifyDataSetChanged();
        }

        private void DisplayMessages()
        {
            MessageAdapter adapter = new MessageAdapter(this, messagesList, loggedUser.UserName, width );
            messagesListView.Adapter = adapter;
            messagesListView.DeferNotifyDataSetChanged();
            
        }
        private void LoadMessages(int alarmId)
        {
            messagesList = WebApiDataController.GetMessages(alarmId);
            if (messagesList.FirstOrDefault().UserName == "failed" && messagesList.FirstOrDefault().Message == "failed") return;
            DisplayMessages();
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.toolbar_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var intent = new Intent(this, typeof(AlarmActivity));
            intent.PutExtra("alarmId", alarm.Id);
            StartActivity(intent);
            return base.OnOptionsItemSelected(item);
        }
    }
}