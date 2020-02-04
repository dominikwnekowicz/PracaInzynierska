using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PwszAlarm.Activities;
using PwszAlarm.Model;

namespace PwszAlarm.Adapters
{
    class MessageAdapter : BaseAdapter
    {

        private ChatActivity chatActivity;
        private List<Messages> messagesList;
        private string userName;
        private int width;

        public MessageAdapter(ChatActivity chatActivity, List<Messages> messagesList, string userName, int width)
        {
            this.chatActivity = chatActivity;
            this.messagesList = messagesList;
            this.userName = userName;
            this.width = width;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater)chatActivity.BaseContext.GetSystemService(Context.LayoutInflaterService);
            View messageView = inflater.Inflate(Resource.Layout.message_layout, null);

            TextView userName, messageTime, sentMessageTextView, recivedMessageTextView;
            userName = messageView.FindViewById<TextView>(Resource.Id.messageUserNameTextView);
            messageTime = messageView.FindViewById<TextView>(Resource.Id.messageTimeTextView);
            sentMessageTextView = messageView.FindViewById<TextView>(Resource.Id.sentMessageTextView);
            recivedMessageTextView = messageView.FindViewById<TextView>(Resource.Id.recivedMessageTextView);


            userName.Text = messagesList[position].UserName;
            var dateCount = DateTime.Today.Day - messagesList[position].MessageTime.Day;
            if (dateCount < 7)
            {
                CultureInfo culture = new CultureInfo("pl-PL");
                if (messagesList[position].MessageTime.Date == DateTime.Now.Date) messageTime.Text = messagesList[position].MessageTime.ToString("HH:mm");
                else messageTime.Text = messagesList[position].MessageTime.ToString("ddd", culture) + " o " + messagesList[position].MessageTime.ToString("HH:mm");
                if (position != 0)
                {
                    var timeDifference = messagesList[position].MessageTime.Subtract(messagesList[position-1].MessageTime).TotalMinutes;
                    if (timeDifference < 10)
                    {
                        messageTime.Visibility = ViewStates.Gone;
                    }
                }
            }
            else
            {
                messageTime.Text = messagesList[position].MessageTime.ToString("dd-MM-yyyy HH:mm");
            }
            sentMessageTextView.SetPadding(Convert.ToInt32(width * 0.06), Convert.ToInt32(width * 0.03), Convert.ToInt32(width * 0.06), Convert.ToInt32(width * 0.03));
            recivedMessageTextView.SetPadding(Convert.ToInt32(width * 0.06), Convert.ToInt32(width * 0.03), Convert.ToInt32(width * 0.06), Convert.ToInt32(width * 0.03));

            if (this.userName == messagesList[position].UserName)
            {
                sentMessageTextView.Text = messagesList[position].Message;
                messageView.SetPadding(Convert.ToInt32(width*0.3), 0, 0, 0);
                userName.Visibility = ViewStates.Gone;
                recivedMessageTextView.Visibility = ViewStates.Gone;
            }
            else
            {
                recivedMessageTextView.Text = messagesList[position].Message;
                messageView.SetPadding(0, 10, Convert.ToInt32(width * 0.3), 0);
                sentMessageTextView.Visibility = ViewStates.Gone;
            }

            return messageView;
        }
        public override int Count
        {
            get
            {
                return messagesList.Count;
            }
        }

    }
}