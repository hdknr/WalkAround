using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using System.Collections.Generic;
using System.Linq;
using Android.Locations;
using Android.Util;


namespace WalkAround.Droid
{
	[Activity (Label = "WalkAround.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new App ());


			((WalkAround.MainContent)WalkAround.App.Current.MainPage).LongitudeLabel.Text = "Longitude";
			((WalkAround.MainContent)WalkAround.App.Current.MainPage).LatitudeLabel.Text = "Latitiude";

			InitializeLocationManager();
		}

		Location _currentLocation;
		LocationManager _locationManager;
		string _locationProvider;
		static readonly string TAG = "X:" + typeof (MainActivity).Name;

		void InitializeLocationManager()
		{
			_locationManager = (LocationManager) GetSystemService(LocationService);
			Criteria criteriaForLocationService = new Criteria
			{
				Accuracy = Accuracy.Fine
			};
			IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

			if (acceptableLocationProviders.Any())
			{
				_locationProvider = acceptableLocationProviders.First();
			}
			else
			{
				_locationProvider = string.Empty;
			}
			Log.Debug(TAG, "Using " + _locationProvider + ".");
		}
	}
}

