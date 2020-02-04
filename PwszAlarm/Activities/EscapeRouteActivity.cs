using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using ImageViews.Photo;
using PwszAlarm.Adapters;
using PwszAlarm.Model;
using PwszAlarm.PwszAlarmDB;

namespace PwszAlarm.Activities
{
    [Activity(Label = "EscapeRouteActivity")]
    public class EscapeRouteActivity : AppCompatActivity
    {
        string choosenRoom, choosenFloor;
        Alarm alarm;
        Room room;
        List<EscapeRoutes> escapeRoutesList, routes;
        readonly List<int> routeImagesId = new List<int>();
        Button goLeft, goRight;
        TextView routeTextView;
        ImageButton nextStep, previousStep;
        ViewPager viewPager;
        PhotoView photoView;
        string way;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EscapeRoute);

            // Create your application here

            goLeft = FindViewById<Button>(Resource.Id.goLeftButton);
            goRight = FindViewById<Button>(Resource.Id.goRightButton);

            nextStep = FindViewById<ImageButton>(Resource.Id.nextStepButton);
            previousStep = FindViewById<ImageButton>(Resource.Id.previousStepButton);

            routeTextView = FindViewById<TextView>(Resource.Id.routeTextView);

            choosenRoom = Intent.GetStringExtra("choosenRoom");
            choosenFloor = Intent.GetStringExtra("choosenFloor");
            room = SQLiteDb.GetRooms(this).FirstOrDefault(r => r.Name == choosenRoom && r.Floor == choosenFloor);
            var alarmId = Intent.GetIntExtra("alarmId", 1);

            alarm = SQLiteDb.GetAlarms(this).GetAwaiter().GetResult().FirstOrDefault(a => a.Id == alarmId);
            var route = new EscapeRoutes();

            viewPager = FindViewById<ViewPager>(Resource.Id.escapeRoutesViewPager);

            if (room.Side != "left" && room.Side != "right" )
            {
                escapeRoutesList = route.GetEscapeRoutes(room.Side);
                ShowRoute();
            }
            else
            {
                goLeft.Click += GoLeft_Click;
                goRight.Click += GoRight_Click;
                AskForWay();
                
            }

            previousStep.Click += PreviousStep_Click;
            nextStep.Click += NextStep_Click;

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.escapeRouteToolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = choosenRoom + " - Ewakuacja";
            SupportActionBar.Subtitle = alarm.Name + " - " + SQLiteDb.GetRooms(this).FirstOrDefault(r => r.Id == alarm.RoomId).Name;
        }

        private void NextStep_Click(object sender, EventArgs e)
        {
            viewPager.SetCurrentItem(viewPager.CurrentItem + 1, false);
        }

        private void PreviousStep_Click(object sender, EventArgs e)
        {
            viewPager.SetCurrentItem(viewPager.CurrentItem - 1, false);
        }

        private void WayChoosen()
        {
            nextStep.Visibility = ViewStates.Visible;
            previousStep.Visibility = ViewStates.Visible;

            goLeft.Visibility = ViewStates.Gone;
            goRight.Visibility = ViewStates.Gone;
            var route = new EscapeRoutes();
            if (way == room.Side)
            {
                escapeRoutesList = route.GetEscapeRoutes("back");
                ShowRoute();
            }
            else
            {
                escapeRoutesList = route.GetEscapeRoutes("front");
                ShowRoute();
            }
        }

        private void AskForWay()
        {
            nextStep.Visibility = ViewStates.Gone;
            previousStep.Visibility = ViewStates.Gone;

            goLeft.Visibility = ViewStates.Visible;
            goRight.Visibility = ViewStates.Visible;

            routeTextView.Text = "W lewo czy w prawo?";
        }

        private void GoRight_Click(object sender, EventArgs e)
        {
            way = "right";
            WayChoosen();
        }

        private void GoLeft_Click(object sender, EventArgs e)
        {
            way = "left";
            WayChoosen();
        }

        private void SetAdapter()
        {
            ImagesScrollViewAdapter adapter = new ImagesScrollViewAdapter(this, routeImagesId);
            viewPager.Adapter = adapter;
            viewPager.PageScrolled += ViewPager_PageScrolled;
        }

        private void ViewPager_PageScrolled(object sender, ViewPager.PageScrolledEventArgs e)
        {            if (e.Position == 0) previousStep.Visibility = ViewStates.Invisible;
            else if (e.Position == (routes.Count - 1)) nextStep.Visibility = ViewStates.Invisible;
            else
            {
                previousStep.Visibility = ViewStates.Visible;
                nextStep.Visibility = ViewStates.Visible;
            }
            if (e.Position >= routes.Count)
            {
                viewPager.SetCurrentItem((routes.Count - 1), false);
                photoView.SetDisplayMatrix(new Matrix());
                photoView.SetSuppMatrix(new Matrix());
            }
            else if (photoView != null)
            {
                photoView.SetDisplayMatrix(new Matrix());
                photoView.SetSuppMatrix(new Matrix());
            }
            if (routes.Count == 1 && (way != "left" || way != "right"))
            {
                var layout = FindViewById<LinearLayout>(Resource.Id.routesControlLinearLayout);
                layout.Visibility = ViewStates.Invisible;
            }
            routeTextView.Text = routes[e.Position].Text;
            photoView = (PhotoView)viewPager.GetChildAt(e.Position);
        }

        private void ShowRoute()
        {
            routes = new List<EscapeRoutes>();
            if (escapeRoutesList.Count > 1)
            {
                var routesToRevers = escapeRoutesList.Where(e => e.Id <= escapeRoutesList.FirstOrDefault(e => e.FloorName == room.Floor).Id).ToList();
                int count = routesToRevers.Count - 1;
                foreach (var route in routesToRevers)
                {
                    routes.Add(routesToRevers[count]);
                    count--;
                }
            }
            else
            {
                routes = escapeRoutesList;
            }

            foreach (var route in routes)
            {
                routeImagesId.Add(route.ImageId);
            }

            SetAdapter();
        }
    }
}