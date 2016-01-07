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

using System.Threading.Tasks;
using System.Text;

namespace WalkAround.Droid
{
	[Activity (Label = "WalkAround.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity, ILocationListener
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


		/// <summary>
		/// Raises the resume event.
		/// </summary>
		protected override void OnResume ()
		{
			base.OnResume ();
			_locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);

		}

		/// <Docs>Called as part of the activity lifecycle when an activity is going into
		///  the background, but has not (yet) been killed.</Docs>
		/// <summary>
		/// Raises the pause event.
		/// </summary>
		protected override void OnPause()
		{
			base.OnPause();
			_locationManager.RemoveUpdates(this);
		}

		async Task<Address> ReverseGeocodeCurrentLocation()
		{
			Geocoder geocoder = new Geocoder(this);
			IList<Address> addressList =
				await geocoder.GetFromLocationAsync(
					_currentLocation.Latitude, 
					_currentLocation.Longitude, 10);

			Address address = addressList.FirstOrDefault();
			return address;
		}

		void DisplayAddress(Address address)
		{
			var label = ((WalkAround.MainContent)WalkAround.App.Current.MainPage).AddressLabel;

			if (address != null)
			{
				StringBuilder deviceAddress = new StringBuilder();
				for (int i = 0; i < address.MaxAddressLineIndex; i++)
				{
					deviceAddress.AppendLine(address.GetAddressLine(i));
				}
				// Remove the last comma from the end of the address.
				label.Text = deviceAddress.ToString();
			}
			else
			{
				label.Text = "Unable to determine the address. Try again in a few minutes.";
			}
		}
	}
}

