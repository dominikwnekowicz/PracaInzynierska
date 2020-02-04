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

namespace PwszAlarm.Adapters
{
    class RoomsListAdapter : BaseExpandableListAdapter
    {

        private readonly List<Room> rooms;

        private readonly List<string> floors = new List<string>
        {
            "Parter",
            "Pierwsze piętro",
            "Drugie piętro"
        };
        private Context context;

        public RoomsListAdapter(Context context, IEnumerable<Room> rooms)
        {
            this.rooms = rooms.ToList();
            this.context = context;
        }

        public override int GroupCount => floors.Count;

        public override bool HasStableIds => false;

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            var result = rooms.Where(r => r.Floor == floors[groupPosition]).ToList();
            return result[childPosition].Name;
            
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            var result = rooms.Where(r => r.Floor == floors[groupPosition]).ToList();
            return result.Count;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            if(convertView == null)
            {
                LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
                convertView = inflater.Inflate(Resource.Layout.room_layout, null);
            }
            TextView textViewItem = convertView.FindViewById<TextView>(Resource.Id.roomTextView);
            string content = (string)GetChild(groupPosition, childPosition);
            textViewItem.Text = content;
            return convertView;

        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return floors[groupPosition];
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            if(convertView == null)
            {
                LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
                convertView = inflater.Inflate(Resource.Layout.rooms_group, null);
            }
            string textGroup = (string)GetGroup(groupPosition);
            TextView textViewGroup = convertView.FindViewById<TextView>(Resource.Id.roomsGroupTextView);
            textViewGroup.Text = textGroup;
            return convertView;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }
    }

    class RoomsListAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}