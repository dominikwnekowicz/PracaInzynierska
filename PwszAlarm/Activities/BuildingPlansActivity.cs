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
using Android.Views;
using Android.Webkit;
using Android.Widget;
using ImageViews.Photo;
using PwszAlarm.Adapters;
using PwszAlarm.Model;
using PwszAlarm.PwszAlarmDB;

namespace PwszAlarm.Activities
{
    [Activity(Label = "BuildingPlansActivity")]
    public class BuildingPlansActivity : Activity
    {
        private class Image
        {
            public int Id { get; set; }
            public string Title { get; set; }

        }
        List<Image> imagesList;
        readonly List<int> imagesIdList = new List<int>();
        TextView imageTitle;
        ImageButton previousImage, nextImage;
        ViewPager viewPager;
        PhotoView photoView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BuildingPlans);
            // Create your application here
            imageTitle = FindViewById<TextView>(Resource.Id.imageTitleTextView);
            previousImage = FindViewById<ImageButton>(Resource.Id.previousImageButton);
            nextImage = FindViewById<ImageButton>(Resource.Id.nextImageButton);

            imagesList = new List<Image>
            {
                new Image {Id = Resource.Drawable.parter, Title="Parter"},
                new Image {Id = Resource.Drawable.pierwszePietro, Title="Pierwsze piętro"},
                new Image {Id = Resource.Drawable.drugiePietro, Title="Drugie piętro"}
            };
            foreach(var image in imagesList)
            {
                imagesIdList.Add(image.Id);
            }
            viewPager = FindViewById<ViewPager>(Resource.Id.buildingsPlanViewPager);
            ImagesScrollViewAdapter adapter = new ImagesScrollViewAdapter(this, imagesIdList);
            viewPager.Adapter = adapter;
            viewPager.PageScrolled += ViewPager_PageScrolled;

            previousImage.Click += PreviousImage_Click;
            nextImage.Click += NextImage_Click;
        }

        private void NextImage_Click(object sender, EventArgs e)
        {
            viewPager.SetCurrentItem(viewPager.CurrentItem + 1, false);
        }

        private void PreviousImage_Click(object sender, EventArgs e)
        {
            viewPager.SetCurrentItem(viewPager.CurrentItem - 1, false);
        }

        private void ViewPager_PageScrolled(object sender, ViewPager.PageScrolledEventArgs e)
        {
            imageTitle.Text = imagesList[e.Position].Title;
            if (e.Position == 0) previousImage.Visibility = ViewStates.Invisible;
            else if (e.Position == (imagesList.Count - 1)) nextImage.Visibility = ViewStates.Invisible;
            else
            {
                previousImage.Visibility = ViewStates.Visible;
                nextImage.Visibility = ViewStates.Visible;
            }
            if (e.Position >= imagesList.Count)
            {
                viewPager.SetCurrentItem((imagesList.Count - 1), false);
                photoView.SetDisplayMatrix(new Matrix());
                photoView.SetSuppMatrix(new Matrix());
            }
            else if (photoView != null)
            {
                photoView.SetDisplayMatrix(new Matrix());
                photoView.SetSuppMatrix(new Matrix());
            }
            photoView = (PhotoView)viewPager.GetChildAt(e.Position);
        }
    }
}