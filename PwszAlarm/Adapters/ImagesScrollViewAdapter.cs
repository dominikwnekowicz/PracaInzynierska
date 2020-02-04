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
using Android.Widget;
using ImageViews.Photo;

namespace PwszAlarm.Adapters
{
    public class ImagesScrollViewAdapter : PagerAdapter
    {
        readonly Context context;
        readonly List<int> imagesList;

        public ImagesScrollViewAdapter(Context context, List<int> imagesList)
        {
            this.context = context;
            this.imagesList = imagesList;
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object objectValue)
        {
            return view == ((ImageView)objectValue);
        }

        [Obsolete]
        public override Java.Lang.Object InstantiateItem(View container, int position)
        {
            PhotoView imageView = new PhotoView(context);

            imageView.SetScaleType(ImageView.ScaleType.CenterInside);
            imageView.SetImageResource(imagesList[position]);
            ((ViewPager)container).AddView(imageView, position);
            return imageView;
        }

        [Obsolete]
        public override void DestroyItem(View container, int position, Java.Lang.Object objectValue)
        {
            ((ViewPager)container).RemoveView((ImageView)objectValue);
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return imagesList.Count;
            }
        }

    }

    class BuildingPlansAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}