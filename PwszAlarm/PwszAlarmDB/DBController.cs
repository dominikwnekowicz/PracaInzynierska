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
using Microsoft.AppCenter;
using Microsoft.AppCenter.Data;

namespace PwszAlarm.PwszAlarmDB
{
    public class DBController : Application
    {
        private async void Start()
        {
            AppCenter.Start("096ac918-fd8d-4887-942a-b29f4c5e5fd0", typeof(Data));
            Room room = new Room();
            var rooms = await Data.ReadAsync<Room>(room.Id.ToString(), DefaultPartitions.UserDocuments);

            var one = rooms;

        }
    }
}