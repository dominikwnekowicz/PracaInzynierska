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
using Plugin.CurrentActivity;

namespace PwszAlarm
{
#if DEBUG
	[Application(AllowBackup = false, Debuggable = true, UsesCleartextTraffic = true)]
#else
	[Application(AllowBackup = true, Debuggable = false, UsesCleartextTraffic = true)]
#endif

	public class MainApplication : Application
	{
		public MainApplication(IntPtr handle, JniHandleOwnership transer)
		  : base(handle, transer)
		{

		}

		public override void OnCreate()
		{
			base.OnCreate();
			CrossCurrentActivity.Current.Init(this);
		}

		public override void OnTerminate()
		{
			base.OnTerminate();
		}

		public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
		{
			CrossCurrentActivity.Current.Activity = activity;
		}
	}
}